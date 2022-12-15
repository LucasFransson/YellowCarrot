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
    /// Interaction logic for RecipeDetailsWindow.xaml
    /// </summary>
    public partial class RecipeDetailsWindow : Window
    {
        Recipe recipe { get; set; }
        public RecipeDetailsWindow(Recipe recipe)
        {
            InitializeComponent();

            this.recipe= recipe;
            lblEditHeadliner.Content = $"Editing {recipe.Name} Recipe";
            tbxRecipeName.Text=recipe.Name;

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.ID); // Jag kunde inte nå Ingredients och Tags via Receptet som jag skickade med, så därför får jag köra en Get.Include metod här.  Det kan gå att lösa via metoder i korrekt _Context (IngredientRepository), checka detta 
              
                AppManager.LoadIngredients(lvRecipe, recipe.Ingredients);
                List<Tag> tags = unitOfWork.Tags.GetAllTags();
                AppManager.LoadObjectsToComboBox(cboTags, tags);
                unitOfWork.Complete();
            }
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                unitOfWork.Complete();
            }
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {

            }
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                Ingredient ingredient = AppManager.CreateIngredient(tbxIngredient.Text, cboUnit.SelectedItem.ToString(), int.Parse(tbxIngredientQuantity.Text)); // fånga null om inget är valt till att bli "";
                AppManager.AddLvItemToLv(AppManager.CreateListViewItem(ingredient, $"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}"), lvRecipe);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWin = new();
            homeWin.Show();
            this.Close();
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                recipe.TagID = null;
                recipe.TagName= null;
            }
        }
        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                //unitOfWork.Ingredients.Remove(AppManager)
                //unitOfWork.Ingredients.RemoveWhere(i=>i.ID==recipe.ID);
                Ingredient ingredient = AppManager.GetIngredientFromListView(lvRecipe);
                unitOfWork.Ingredients.Remove(ingredient);
                unitOfWork.Complete(); // För att uppdatera så ui kan refresha

                // Lagt in detta såhär för att testa. Gör en Update UI metod
                recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.ID); 
                AppManager.LoadIngredients(lvRecipe, recipe.Ingredients);
                unitOfWork.Complete();
            }
        }

        private void btnRemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show($"Are you sure you want to remove the recipe : {recipe.Name} ?" , "Remove Recipe" , MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var unitOfWork = new UnitOfWork(new AppDbContext()))
                {
                    unitOfWork.Recipes.Remove(recipe);
                    //unitOfWork.Recipes.RemoveWhere(r=>r.ID==recipe.ID);
                    unitOfWork.Complete();
                    MessageBox.Show($"You have removed the recipe : {recipe.Name}!", "Recipe Removed");
                    HomeWindow homeWin = new();
                    homeWin.Show();
                    this.Close();
                }
            }
        }
    }
}
