using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Interfaces;
using YellowCarrot.Models;

namespace YellowCarrot.Managers
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
            : base(context)
        {
            this._context = context;
        }

        public List<Recipe> GetAllRecipesWithIngredients()
        {
            return _context.Recipes.Include(r=>r.Ingredients).ToList();
        }


        public User GetRecipeUserByRecipeID(int recipeID,UserRepository userRepo)
        {
            return userRepo.FindById(recipeID);
            //_context.Recipes.FirstOrDefault(r=>r.ID==recipeID)
        }

        public Recipe GetRecipeWithIngredients(int recipeID)
        {
            return _context.Recipes.Include(r => r.Ingredients).First(r => r.ID == recipeID);
        }

    }
}
