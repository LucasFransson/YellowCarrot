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
                    //List<Recipe> recipes = context.Recipes.Include(r => r.Ingredients).ToList();
                    ////List<Recipe> recipes = unitOfWork.Recipes.GetAll().ToList();
                    //foreach (Recipe recipe in recipes)
                    //{
                    //    recipe.Ingredients.ForEach(i => lvCurrentRecipe.Items.Add(i.Name));
                    //}
                    unitOfWork.Complete();
                }
            }

        }

        private void btnOpenRecipe_Click(object sender, RoutedEventArgs e)
        {
           
            Recipe recipe = AppManager.GetRecipeFromListView(lvRecipes);
            //using(var unitOfWork = new UnitOfWork(new AppDbContext()))
            //{
            //    List<Ingredient> ingredients = AppManager.GetIngredientsFromRecipe(recipe,unitOfWork);
            //    AppManager.LoadIngredients(lvCurrentRecipe, ingredients);
            //    unitOfWork.Complete();
            //}

            using(var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.ID);
                AppManager.LoadIngredients(lvCurrentRecipe, recipe.Ingredients);
                unitOfWork.Complete();
            }

            //typ Working?
            //using (AppDbContext context = new())
            //{
            //    using (var unitOfWork = new UnitOfWork(context))
            //    {
            //        AppManager.LoadAllRecipes(lvRecipes, unitOfWork);
            //        List<Recipe> recipes = context.Recipes.Where(r=>r.ID==recipe.ID).Include(r => r.Ingredients).ToList();
            //        //Recipe rec = (Recipe)context.Recipes.Where(r => r.ID == recipe.ID).Include(r => r.Ingredients);
            //        //List<Recipe> recipes = unitOfWork.Recipes.GetAll().ToList();
            //        foreach (var i in recipes)
            //        {
            //            recipe.Ingredients.ForEach(i => lvCurrentRecipe.Items.Add(i.Name));
            //        }
            //        unitOfWork.Complete();
            //    }
            //}
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
    }
}
