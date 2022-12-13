using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YellowCarrot.Data;
using YellowCarrot.Managers;
using YellowCarrot.Models;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new();
            mainWin.Show();
            this.Close();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            using (var userRepo = new UserRepository(new UserDbContext()))
            {
                if (AppManager.CheckRegisterRequirements(tbxUsername.Text, tbxPassword.Text,userRepo))
                {
                    User user = AppManager.CreateUser(tbxFirstName.Text,tbxLastName.Text,tbxUsername.Text, tbxPassword.Text);
                    userRepo.Add(user);
                    userRepo.Complete();
                    MessageBox.Show("You have succesfully signed up!");
                    MainWindow mainWin = new();
                    mainWin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your Username or Password did not meet the requirements. Please try again.");
                }
            }
        }
    }
}
