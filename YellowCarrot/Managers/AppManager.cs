using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Policy;
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


        // Adds a listviewitem to a listview
        public static void AddLvItemToLv(ListViewItem lvItem,ListView listView)
        {
            listView.Items.Add(lvItem);
        }
        // Takes a object and a string and creates a listviewitem from it with the object as .Tag and the string content as .Content
        public static ListViewItem CreateListViewItem(Object obj,string objContent)
        {
            ListViewItem lvItem = new();
            lvItem.Tag = obj;
            lvItem.Content = objContent;
            return lvItem;
        }

        // Utmarkerad pga unused
        //public static List<Ingredient> GetIngredientsFromLv(ListView listView)
        //{
        //    List<Ingredient> ingredients = new();
        //    foreach(var item in listView.Items)
        //    {
        //        ListViewItem selectedItem = item as ListViewItem;
        //        ingredients.Add(selectedItem.Tag as Ingredient);
        //    }
        //    return ingredients;
        //}

        // Utmarkerad pga unused
        //public static List<Ingredient> GetIngredientsFromRecipe(Recipe _recipe,UnitOfWork unitOfWork)
        //{
        //    return unitOfWork.Recipes.FindById(_recipe.ID).Ingredients.ToList();
        //}

        // Takes a listview, Creates an empty generic list, and then fill the list with all the objects from the listview
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
        // Takes a string, then creates a Tag object with the name
        public static Tag CreateTag(string name)
        {
            Tag tag = new();
            tag.Name = name;
            return tag;
        }
        // Takes a string name, string unit, int quantity, trims the strings, and then Creates an inswstance of Ingredient 
        public static Ingredient CreateIngredient(string name,string unit, int quantity)
        {
            Ingredient ingredient = new();
            ingredient.Name = name.Trim();
            ingredient.Unit = unit.Trim();
            ingredient.Quantity= quantity;
            return ingredient;
        }
        // Takes a string and a list of Ingredients, Creates an empty _recipe, iterates through the list of ingredients and adds the ingredients to the _recipe
        // Trims the string and assign it as the Recipe.Name property, Takes the int from AppManager.LoggedInUser.ID and assign it to the Recipe.UserID
        public static Recipe CreateRecipe(string recipeName, List<Ingredient> ingredients)
        {
            Recipe recipe = new();
            foreach (Ingredient ingredient in ingredients)
            {
                recipe.Ingredients.Add(ingredient);
            }
            recipe.Name = recipeName.Trim();
            recipe.UserID = AppManager.LoggedInUser.ID;
            return recipe;
        }

        public static Recipe EditRecipe(Recipe recipe,string recipeName, List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                recipe.Ingredients.Add(ingredient);
            }
            recipe.Name = recipeName.Trim();
            recipe.UserID = AppManager.LoggedInUser.ID;
            return recipe;
        }
        public static void AddIngredientsToRecipe(Recipe recipe, List<Ingredient>ingredients)
        {
            foreach(Ingredient ingredient in ingredients)
            {
                recipe.Ingredients.Add(ingredient);
            }
        }

        // utmarkerad pga unused
        //public static Recipe CreateRecipe(string recipeName,params Ingredient[] ingredients)
        //{
        //    Recipe _recipe = new();
        //    foreach(Ingredient ingredient in ingredients)
        //    {
        //        _recipe.Ingredients.Add(ingredient);
        //    }
        //    _recipe.Name=recipeName;
        //   // _recipe.UserID = (int)AppManager.UserID;
        //    return _recipe;
        //}

        // Takes 4 strings (firstname,lastname,username,password), Create an empty user object, trims the strings and assign them to the user object and returns the object
        public static User CreateUser(string firstname, string lastname,string username, string password)
        {
            User user = new();
            user.FirstName = firstname.Trim();
            user.LastName = lastname.Trim();
            user.UserName = username.Trim();
            user.Password = password.Trim();
            return user;
        }

        // Takes a string(username) and an instance of UserRepository, create a user object, call the FindByUserName method from UserRepository and assign content to user object, assign the user to the static AppManager.LoggedInUser, then saves the database
        public static void LogInUser(string username,UserRepository userRepo)
        {
            User? user = userRepo.FindByUserName(username);
            LoggedInUser = user;
            userRepo.Complete();     // Pointless?
        }
        // Takes 2 strings(username,password) and an instance of UserRepository, checks if either of the strings are null, returns true if they are not, and false if they are
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
        // Takes a string(username), checks if the length is equal or larger than 4, returns true if it is, and false if it's not
        public static bool CheckUsernameRequirements(string username)
        {
            if (username.Length >= 4)
            {
                return true;
            }
            return false;
        }
        // Takes a string(password), checks if the length is equal or larger than 4, returns true if it is, and false if it's not
        public static bool CheckPasswordRequirements(string password)
        {
            if (password.Length >= 4)
            {
                return true;
            }
            return false;
        }
        // Takes a string(username) and an instance of UserRepository, calls the UserRepository.FindByUserName method with the string(username), if it returns null (if the username is not in the database) return true, else return false
        public static bool CheckUsernameAvailability(string username, UserRepository userRepo)
        {
                if (userRepo.FindByUserName(username) == null)
                {
                    return true;
                }
            return false;
        }
        // Takes 2 string(username,password) and an instance of UserRepository, calls the UserRepository.FindByUserName method with the string(username), if it returns an user check that users password property with the string(password), if it matches return true, else return false
        public static bool CheckLogIn(string username, string password,UserRepository userRepo)
        {
            User user = userRepo.FindByUserName(username);
            if(user != null && user.Password==password)
            {
                return true;
            }
            return false;
        }
        public static bool IsRecipeOwnedByUser(ListView listView)
        {
            // Går att optimera med en GetUserFromListView
            Recipe? recipe = AppManager.GetRecipeFromListView(listView);
            if (recipe != null)
            {
                if (recipe.UserID == LoggedInUser.ID) 
                {
                    return true;
                }
            }
            return false;
        }
        // De här lilla QoL metoden kontrollerar endast ändstavelsen av username för ett "s" och justerar strängen vid behov
        // Metoden returnerar en bool som är true om namnet slutar på s, 
        public static bool CheckIfUsernameEndsWithS(string username)
        {
            if (username.EndsWith('s'))
            {
                return true;
            }
            return false;
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
        public static Ingredient GetIngredientFromListView(ListView listView)
        {
            if (listView.SelectedItem != null)
            {
                ListViewItem selectedItem = listView.SelectedItem as ListViewItem;
                Ingredient? selectedIngredient = selectedItem.Tag as Ingredient;
                return selectedIngredient;
            }
            return null;
        }
        public static void DisplayNullRecipesFoundListView(ListView listView)
        {
            listView.Items.Clear();
            string s = "No Recipes Found";
            listView.Items.Add(s);
            //CreateListViewItem(s,s);
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


        // Metoden avser främst att ladda upp tidigare skapade Tags till en combobox, men jag valde att göra listan generell
        public static void LoadObjectsToComboBox<T>(ComboBox cbo, List<T> list)
        {
            foreach (var item in list)
            {
                cbo.Items.Add(item.ToString());
            }
        }


        public static void LoadRecipeListToListView(List<Recipe> recipes, ListView listView)
        {
            listView.Items.Clear();
            foreach(var recipe in recipes)
            {
                AppManager.AddLvItemToLv(AppManager.CreateListViewItem(recipe, $"{recipe.Name}"), listView);
            }
        }
        public static void LoadAllRecipes(ListView listView,UnitOfWork unitOfWork)
        {
            listView.Items.Clear();
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
        public static void LoadTags (ComboBox cbo,UnitOfWork unitOfWork)
        {
            List<Tag> tags = unitOfWork.Tags.GetAll().ToList();
            foreach(var t in tags)
            {
                cbo.Items.Add(t.Name);
            }
        }
    }
}
