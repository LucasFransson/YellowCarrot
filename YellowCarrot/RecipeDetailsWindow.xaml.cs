﻿using System;
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
            AppManager.LoadIngredients(lvRecipe, recipe.Ingredients);
        }
    }
}
