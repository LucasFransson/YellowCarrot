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
                recipe = unitOfWork.Recipes.GetRecipeWithIngredients(recipe.ID);
                tbxRecipeName.Text = recipe.Name;
              
                AppManager.LoadIngredients(lvRecipe, recipe.Ingredients);
                List<Tag> tags = unitOfWork.Tags.GetAllTags();
                AppManager.LoadObjectsToComboBox(cboTags, tags);
                unitOfWork.Complete();
            }
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveRecipe_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
