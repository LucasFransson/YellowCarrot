using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Managers;
using YellowCarrot.Models;

namespace YellowCarrot.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        List<Recipe> GetAllRecipesWithIngredients();
        Recipe GetRecipeWithIngredients(int recipeID);
        Recipe GetRecipeIncluded(int recipeID);
        User GetUserByRecipeID(int recipeID,UserRepository userRepo);
        string GetUserNameByRecipeId(int recipeID,UserRepository userRepo);

        List<Recipe> GetRecipesByUserName(string userName, UserRepository userRepo);
        List<Recipe> GetRecipesByIngredient(string ingredientName);
        List<Recipe> GetRecipesByTag(string tag);
        List<Recipe> GetRecipesByRecipeName(string recipeName);
        List<Recipe> GetRecipesByUserID(int userID);

    }
}
