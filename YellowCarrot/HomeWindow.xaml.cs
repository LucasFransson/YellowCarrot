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
using YellowCarrot.Interfaces;
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
                    AppManager.LoadTags(cboTags, unitOfWork);
                    unitOfWork.Complete();
                }
            }
        }

        private void btnOpenRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (lvRecipes.SelectedItem != null)
            {
                Recipe recipe = AppManager.GetRecipeFromListView(lvRecipes);

                using (var unitOfWork = new UnitOfWork(new AppDbContext()))
                {
                    recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.ID);
                    lblCurrentRecipe.Content = recipe.Name;
                    lblRecipeByUser.Content = $"Recipe By: {unitOfWork.Recipes.GetUserNameByRecipeId(recipe.ID, new UserRepository(new UserDbContext()))}";
                    AppManager.LoadIngredients(lvCurrentRecipe, recipe.Ingredients);
                    unitOfWork.Complete();
                }
            }
            else { MessageBox.Show("You must select a _recipe from the list to open!"); }
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
            Recipe recipe = AppManager.GetRecipeFromListView(lvRecipes);
            if (recipe != null)
            {
                RecipeDetailsWindow detailsWin = new(recipe);
                detailsWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You must select a _recipe thats was created by You to open the Recipe Editor.");
            }
        }

        private void btnMyRecipes_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                AppManager.LoadRecipeListToListView(unitOfWork.Recipes.GetRecipesByUserID(AppManager.LoggedInUser.ID),lvRecipes);
                unitOfWork.Complete();
            }
            if (AppManager.CheckIfUsernameEndsWithS(AppManager.LoggedInUser.UserName)) { lblRecipes.Content = $"{AppManager.LoggedInUser.UserName} Recipes"; }
            else { lblRecipes.Content = $"{AppManager.LoggedInUser.UserName}s Recipes"; }
        }

        private void btnShowAllRecipes_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                AppManager.LoadAllRecipes(lvRecipes, unitOfWork);
                unitOfWork.Complete();
            }
            lblRecipes.Content = $"All Recipes";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                using (var userRepo = new UserRepository(new UserDbContext()))
                {
                    switch (true)
                    {
                         case true when rbtnSearchRecipe.IsChecked == true:
                            {
                                
                                List<Recipe> recipes = unitOfWork.Recipes.GetRecipesByRecipeName(tbxSearchInput.Text);
                                if (recipes.Count > 0)
                                {
                                    AppManager.LoadRecipeListToListView(recipes, lvRecipes);
                                }
                                else
                                {
                                    AppManager.DisplayNullRecipesFoundListView(lvRecipes);
                                }
                                break;
                            }
                        case true when rbtnSearchTag.IsChecked == true:
                            {
                                List<Recipe> recipes = unitOfWork.Recipes.GetRecipesByTag(cboTags.SelectedItem.ToString()); // funkar ej
                                if (recipes.Count > 0)
                                {
                                    AppManager.LoadRecipeListToListView(recipes, lvRecipes);
                                }
                                else
                                {
                                    AppManager.DisplayNullRecipesFoundListView(lvRecipes);
                                }
                                break;
                            }
                        case true when rbtnSearchUser.IsChecked == true:
                            {
                                List<Recipe> recipes = unitOfWork.Recipes.GetRecipesByUserName(tbxSearchInput.Text,userRepo);
                                if (recipes.Count > 0)
                                {
                                    AppManager.LoadRecipeListToListView(recipes, lvRecipes);

                                    if (AppManager.CheckIfUsernameEndsWithS(tbxSearchInput.Text)) { lblRecipes.Content = $"{tbxSearchInput.Text} Recipes"; }
                                    else { lblRecipes.Content = $"{tbxSearchInput.Text}s Recipes"; }
                                }
                                else
                                {
                                    AppManager.DisplayNullRecipesFoundListView(lvRecipes);
                                }
                                break;
                            }
                        case true when rbtnSearchIngredient.IsChecked == true:
                            {
                                List<Recipe> recipes = unitOfWork.Recipes.GetRecipesByIngredient(tbxSearchInput.Text);
                                if (recipes.Count > 0)
                                {
                                    AppManager.LoadRecipeListToListView(recipes, lvRecipes);
                                }
                                else
                                {
                                    AppManager.DisplayNullRecipesFoundListView(lvRecipes);
                                }
                                break;
                            }
                        default: break;
                    }
                }
            }
        }


        private void rbtnSearchRecipe_Click(object sender, RoutedEventArgs e)
        {
            cboTags.Visibility = Visibility.Collapsed;
            tbxSearchInput.Visibility = Visibility.Visible;
        }

        private void rbtnSearchTag_Click(object sender, RoutedEventArgs e)
        {
            tbxSearchInput.Visibility= Visibility.Collapsed;
            cboTags.Visibility= Visibility.Visible;
            
        }

        private void rbtnSearchUser_Click(object sender, RoutedEventArgs e)
        {
            cboTags.Visibility = Visibility.Collapsed;
            tbxSearchInput.Visibility = Visibility.Visible;
        }

        private void rbtnSearchIngredient_Click(object sender, RoutedEventArgs e)
        {
            cboTags.Visibility = Visibility.Collapsed;
            tbxSearchInput.Visibility = Visibility.Visible;
        }

        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AppManager.IsRecipeOwnedByUser(lvRecipes))
            {
                btnEditRecipe.Visibility= Visibility.Visible;
            }
            else
            {
                btnEditRecipe.Visibility = Visibility.Collapsed;
            }
        }

        private void lvCurrentRecipe_LostFocus(object sender, RoutedEventArgs e)
        {
            btnOpenRecipe.Visibility= Visibility.Collapsed;
        }
    }
}
