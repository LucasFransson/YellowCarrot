using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        List<Recipe> GetAllRecipesWithIngredients();
        Recipe GetRecipeWithIngredients(int recipeID);
    }
}
