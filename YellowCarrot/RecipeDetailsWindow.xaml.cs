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
    public partial class RecipeDetailsWindow : Window   // allt här borde refaktorerats men tiden räcker inte till tyvärr. UI Elementen som hanterar Taggar är hidden, men en del av logiken finns kvar här.
    {
        Recipe _recipe; 
        List<Ingredient> _recipeIngredients = new(); // Listan är till för att kunna lösa problem som uppstod när ingredienser skulle tas bort från listviewen dynamiskt men endast raderas ur DB vid btnSave.Click
        List<Ingredient> _ingredientsToRemove = new(); // samma som ovan
        List<Ingredient> _ingredientsToAdd = new(); 
        public RecipeDetailsWindow(int recipeID)
        {
            InitializeComponent();

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                _recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipeID); // Jag kunde inte nå Ingredients och Tags via Receptet som jag skickade med, så därför får jag köra en Get.Include metod här.  Det kan gå att lösa via metoder i korrekt _Context (IngredientRepository), checka detta 
                
                lblEditHeadliner.Content = $"Editing {_recipe.Name} Recipe";
                tbxRecipeName.Text = _recipe.Name;
                AppManager.LoadUnitEnums(cboUnit); // loads all the available strings from the enum Units to the combobox

                AppManager.LoadIngredients(lvRecipe, _recipe.Ingredients);
                List<Tag> tags = unitOfWork.Tags.GetAllTags();
                AppManager.LoadObjectsToComboBox(cboTags, tags);
                _recipeIngredients = _recipe.Ingredients; 
                
            }
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                if (_ingredientsToRemove.Count > 0)
                {
                    foreach (var i in _ingredientsToRemove)
                    {
                        unitOfWork.Ingredients.Remove(i);
                    }
                }

                Recipe recipe = unitOfWork.Recipes.FindById(_recipe.ID);
                if(recipe != null)
                {
                    _ingredientsToAdd.ForEach(i => recipe.Ingredients.Add(i));

                    if (recipe.Name != tbxRecipeName.Text)
                    {
                        recipe.Name = tbxRecipeName.Text;
                    }
                }
               
                unitOfWork.Complete();

                MessageBox.Show($"You have succesfully edited the {_recipe.Name} Recipe!");
                HomeWindow homeWin = new();
                homeWin.Show();
                this.Close();
            }
        }
        private void btnAddTag_Click(object sender, RoutedEventArgs e) // Inte hunnit implementera detta 
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {

            }
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            bool isNumberOk = int.TryParse(tbxIngredientQuantity.Text, out int number);
            if (isNumberOk)
            {
                Ingredient ingredient = AppManager.CreateIngredient(tbxIngredient.Text, cboUnit.SelectedItem.ToString(), number/*int.Parse(tbxIngredientQuantity.Text)*/); // fånga null om inget är valt till att bli "";
                AppManager.AddLvItemToLv(AppManager.CreateListViewItem(ingredient, $"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}"), lvRecipe);
                _ingredientsToAdd.Add(ingredient);
            }
            tbxIngredient.Text = "";
            tbxIngredientQuantity.Text = "";
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWin = new();
            homeWin.Show();
            this.Close();
        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e) // inte hunnit implementera/fixa detta
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                _recipe.TagID = null;
                _recipe.TagName= null;
            }
        }
        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
      
            Ingredient ingredient = AppManager.GetIngredientFromListView(lvRecipe);
            lvRecipe.Items.Remove(ingredient);
            _recipeIngredients.Remove(ingredient);
            _ingredientsToRemove.Add(ingredient);
            AppManager.LoadIngredients(lvRecipe, _recipeIngredients);

            //    // Koden nedan tar bort ingrediensen direkt utan att behöva savea, ändra lite snabbt så koden passsar bättre med resten av appens logik
            //using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            //{ 
            //    //Ingredient ingredient = AppManager.GetIngredientFromListView(lvRecipe);
            //    //unitOfWork.Ingredients.Remove(ingredient);
            //    //unitOfWork.Complete(); // För att uppdatera så ui kan refresha

            //    //// Lagt in detta såhär för att testa. Gör en Update UI metod
            //    //_recipe = unitOfWork.Recipes.GetRecipeWithIngredients(_recipe.ID); 
            //    //AppManager.LoadIngredients(lvRecipe, _recipe.Ingredients);
            //    //unitOfWork.Complete();
            //}
        }

        private void btnRemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show($"Are you sure you want to remove the recipe : {_recipe.Name} ?" , "Remove Recipe" , MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var unitOfWork = new UnitOfWork(new AppDbContext()))
                {
                    unitOfWork.Recipes.Remove(_recipe);
                    //unitOfWork.Recipes.RemoveWhere(r=>r.ID==_recipe.ID);
                    unitOfWork.Complete();
                    MessageBox.Show($"You have removed the recipe : {_recipe.Name}!", "Recipe Removed");
                    HomeWindow homeWin = new();
                    homeWin.Show();
                    this.Close();
                }
            }
        }
    }
}
