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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            AppManager.LoadUnitEnums(cboUnit);

        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            List<Ingredient> ingredients = AppManager.GetListFromLv<Ingredient>(lvRecipe);
            Recipe recipe = AppManager.CreateRecipe(tbxRecipeName.Text,ingredients);

            using(var unitOfWork = new UnitOfWork(new AppDbContext()))
            {

                unitOfWork.Recipes.Add(recipe);
                unitOfWork.Complete();
                
            }
            MessageBox.Show($"You have added a {recipe.Name} Recipe!");
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWin = new();
            homeWin.Show();
            this.Close();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = AppManager.CreateIngredient(tbxIngredient.Text, cboUnit.SelectedItem.ToString(), int.Parse(tbxIngredientQuantity.Text));

            AppManager.AddLvItemToLv(AppManager.CreateListViewItem(ingredient,$"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}"), lvRecipe);

        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
