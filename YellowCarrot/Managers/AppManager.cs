using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YellowCarrot.Data;
using YellowCarrot.Enums;
using YellowCarrot.Interfaces;
using YellowCarrot.Models;

namespace YellowCarrot.Managers
{
    public static class AppManager
    {
        public static User? LoggedInUser { get; set; }


        public static void AddLvItemToLv(ListViewItem lvItem,ListView listView)
        {
            listView.Items.Add(lvItem);
        }
        public static ListViewItem CreateListViewItem(Object obj,string objContent)
        {
            ListViewItem lvItem = new();
            lvItem.Tag = obj;
            lvItem.Content = objContent;
            return lvItem;
        }
        public static List<Ingredient> GetIngredientsFromLv(ListView listView)
        {
            List<Ingredient> ingredients = new();
            foreach(var item in listView.Items)
            {
                ListViewItem selectedItem = item as ListViewItem;
                ingredients.Add(selectedItem.Tag as Ingredient);
            }
            return ingredients;
        }
        public static List<Ingredient> GetIngredientsFromRecipe(Recipe recipe,UnitOfWork unitOfWork)
        {
            return unitOfWork.Recipes.FindById(recipe.ID).Ingredients.ToList();
        }

        public static List<T> GetListFromLv<T> (ListView listView) where T : class
        {
            List<T> list = new();
            foreach (var item in listView.Items)
            {
                ListViewItem selectedItem = item as ListViewItem;
                list.Add(selectedItem.Tag as T);
            }
            return list;
        }

        public static Tag CreateTag(string name)
        {
            Tag tag = new();
            tag.Name = name;
            return tag;
        }
        public static Ingredient CreateIngredient(string name,string unit, int quantity)
        {
            Ingredient ingredient = new();
            ingredient.Name = name;
            ingredient.Unit = unit;
            ingredient.Quantity= quantity;
            return ingredient;
        }
        public static Recipe CreateRecipe(string recipeName, List<Ingredient> ingredients)
        {
            Recipe recipe = new();
            foreach (Ingredient ingredient in ingredients)
            {
                recipe.Ingredients.Add(ingredient);
            }
            recipe.Name = recipeName;
            recipe.UserID = AppManager.LoggedInUser.ID;
            return recipe;

        }

        //public static Recipe CreateRecipe(string recipeName,params Ingredient[] ingredients)
        //{
        //    Recipe recipe = new();
        //    foreach(Ingredient ingredient in ingredients)
        //    {
        //        recipe.Ingredients.Add(ingredient);
        //    }
        //    recipe.Name=recipeName;
        //   // recipe.UserID = (int)AppManager.UserID;
        //    return recipe;
        //}
        public static User CreateUser(string firstname, string lastname,string username, string password)
        {
            User user = new();
            user.FirstName = firstname;
            user.LastName = lastname;
            user.UserName = username;
            user.Password = password;
            return user;
        }
        public static void LogInUser(string username,UserRepository userRepo)
        {
            User? user = userRepo.FindByUserName(username);
            LoggedInUser = user;
            userRepo.Complete();
            
        }
        public static bool CheckRegisterRequirements(string username, string password,UserRepository userRepo)
        {
            if (username != null && password != null)
            {
                if (CheckUsernameAvailability(username,userRepo) && CheckUsernameRequirements(username) && CheckPasswordRequirements(password))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckUsernameRequirements(string username)
        {
            if (username.Length >= 4)
            {
                return true;
            }
            return false;
        }

        public static bool CheckPasswordRequirements(string password)
        {
            if (password.Length >= 4)
            {
                return true;
            }
            return false;
        }
        public static bool CheckUsernameAvailability(string username, UserRepository userRepo)
        {
                if (userRepo.FindByUserName(username) == null)
                {
                    userRepo.Complete();
                    return true;
                }
            return false;
        }

        public static bool CheckLogIn(string username, string password,UserRepository userRepo)
        {
                if (userRepo.FindByUserName(username).Password == password)
                {
                    userRepo.Complete();
                    return true;
                }
            return false;
        }
        public static void LoadUnitEnums(ComboBox cbo)
        {
            cbo.Items.Add("");
            foreach(var unit in Enum.GetNames(typeof(Units)))
            {
                cbo.Items.Add(unit);
            }
            //cbo.ItemsSource = Enum.GetNames(typeof(Units)); // Jag använder foreach nu istället för jag vill ha ett cbo alternativ utifrån Enums
        }


        public static Recipe GetRecipeFromListView(ListView listView)
        {
            if (listView.SelectedItem != null)
            {
                ListViewItem selectedItem = listView.SelectedItem as ListViewItem;
                Recipe? selectedRecipe = selectedItem.Tag as Recipe;
                return selectedRecipe;
            }
            return null;
        }

        public static void LoadAllRecipes(ListView listView,UnitOfWork unitOfWork)
        {
            foreach (var recipe in unitOfWork.Recipes.GetAll())
            {
                AppManager.AddLvItemToLv(AppManager.CreateListViewItem(recipe, $"{recipe.Name}"), listView);
            }
        }
        public static void LoadIngredients(ListView listView,List<Ingredient> ingredients)
        {
            listView.Items.Clear();
            foreach (var ingredient in ingredients)
            {
                AppManager.AddLvItemToLv(AppManager.CreateListViewItem(ingredient, $"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}"), listView);
            }
        }
    }
}
