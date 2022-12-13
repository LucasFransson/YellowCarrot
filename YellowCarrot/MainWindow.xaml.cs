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
using System.Windows.Navigation;
using System.Windows.Shapes;
using YellowCarrot.Data;
using YellowCarrot.Managers;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new();
            regWin.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var userRepo = new UserRepository(new UserDbContext()))
            {
                if (AppManager.CheckLogIn(tbxUserName.Text, pbxPassword.Password, userRepo))
                {
                    AppManager.LogInUser(tbxUserName.Text, userRepo);
                    HomeWindow homeWin = new();
                    homeWin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your information was not correct.");
                }
            }
        }
    }
}
