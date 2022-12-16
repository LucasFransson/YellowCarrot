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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                AppManager.LoadUnitEnums(cboUnit);
                List<Tag> tags = unitOfWork.Tags.GetAllTags();
                cboTags.Items.Add("Create New Tag");
                AppManager.LoadObjectsToComboBox(cboTags, tags);
            }
        }
        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            List<Ingredient> ingredients = AppManager.GetListFromLv<Ingredient>(lvRecipe);
            Recipe recipe = AppManager.CreateRecipe(tbxRecipeName.Text,ingredients);

            using(var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                if (tbxTag.Text.Count() != 0 && cboTags.SelectedItem.ToString()=="Create New Tag") 
                {
                    Tag tag = AppManager.CreateTag(tbxTag.Text);
                    recipe.TagName= tag.Name;
                    unitOfWork.Tags.Add(tag);
                    //unitOfWork.Complete();
                    //_recipe.TagID= tag.ID;
                }
                else if(cboTags.SelectedItem!= null) 
                {
                    //_recipe.TagName = cboTags.SelectedItem.ToString();
                    Tag tag = unitOfWork.Tags.GetTagByName(cboTags.SelectedItem.ToString());
                    recipe.TagName= tag.Name;
                    unitOfWork.Tags.Add(tag);
                }
                unitOfWork.Recipes.Add(recipe);
                unitOfWork.Complete();
            }
            MessageBox.Show($"You have added a {recipe.Name} Recipe!");
            HomeWindow homeWin = new();
            homeWin.Show();
            this.Close();
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWin = new();
            homeWin.Show();
            this.Close();
        }
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            //foreach(char c in tbxIngredient.Text)
            //{
            //    int number = 0;
            //    if(char.IsNumber(c))
            //    {
            //        number += int.Parse(c);
            //    }
            //}
            foreach(var item in lvRecipe.Items)
            {
                if(item.ToString().Contains(tbxIngredient.Text))
                {
                    MessageBox.Show($"You have already added {tbxIngredient.Text} to the _recipe!");
                    return;
                } 
            }
            bool isNumberOk = int.TryParse(tbxIngredientQuantity.Text, out int number);
            if (isNumberOk)
            {
                Ingredient ingredient = AppManager.CreateIngredient(tbxIngredient.Text, cboUnit.SelectedItem.ToString(), number/*int.Parse(tbxIngredientQuantity.Text)*/); // fånga null om inget är valt till att bli "";
                AppManager.AddLvItemToLv(AppManager.CreateListViewItem(ingredient, $"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}"), lvRecipe);
            }

            tbxIngredient.Text = "";
            tbxIngredientQuantity.Text = "";
            
        }
        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = AppManager.CreateTag(tbxTag.Text);
            using(var unitOfWork = new UnitOfWork(new AppDbContext()))
            {

            }
        }

        private void cboTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbxTag.Text = "";
        }
    }
}
