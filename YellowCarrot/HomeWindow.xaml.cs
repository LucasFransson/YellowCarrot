using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            lblUser.Content = AppManager.LoggedInUser.UserName;

            using (AppDbContext context = new())
            {
                using (var unitOfWork = new UnitOfWork(context))
                {
                    AppManager.LoadAllRecipes(lvRecipes, unitOfWork);
                    unitOfWork.Complete();
                }
            }
        }

        private void btnOpenRecipe_Click(object sender, RoutedEventArgs e)
        { 
            Recipe recipe = AppManager.GetRecipeFromListView(lvRecipes);

            using(var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.ID);
                lblCurrentRecipe.Content= recipe.Name;
                lblRecipeByUser.Content = $"Recipe By: {unitOfWork.Recipes.GetUserNameByRecipeId(recipe.ID,new UserRepository(new UserDbContext ()))}";
                AppManager.LoadIngredients(lvCurrentRecipe, recipe.Ingredients);
                unitOfWork.Complete();
            }
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addWin = new();
            addWin.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            AppManager.LoggedInUser = null;
            //AppManager.UserID= null;
            MainWindow mainWin = new();
            mainWin.Show();
            this.Close();
        }

        private void btnEditRecipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMyRecipes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowAllRecipes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
