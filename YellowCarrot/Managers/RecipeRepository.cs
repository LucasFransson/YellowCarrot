using Azure;
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
        public User GetUserByRecipeID(int recipeID,UserRepository userRepo)
        {
            return userRepo.FindById(recipeID);
            //_context.Recipes.FirstOrDefault(r=>r.ID==recipeID)
        }
        public User GetUserByUserName(string userName, UserRepository userRepo)
        {
            return userRepo.FindByUserName(userName);
        }
        public List<Recipe> GetRecipesByUserName(string userName, UserRepository userRepo)
        {
            User user = GetUserByUserName(userName, userRepo);
            return _context.Recipes.Where(r => r.UserID == user.ID).ToList();
        }
        public Recipe GetRecipeWithIngredients(int recipeID)
        {
            return _context.Recipes.Include(r => r.Ingredients).First(r => r.ID == recipeID);
        }
        public Recipe GetRecipeIncluded(int recipeID) // error pga tag id != key
        {
            return _context.Recipes.Include(r => r.Ingredients).Include(r=>r.TagID).First(r => r.ID == recipeID); 
        }
        public List<Recipe> GetRecipesByRecipeName(string recipeName)
        {
            return _context.Recipes.Where(r=>r.Name == recipeName).ToList();
        }
        public List<Recipe> GetRecipesByUserID(int userID)
        {
            return _context.Recipes.Where(r=>r.UserID==userID).ToList();
        }
        public List<Recipe> GetRecipesByTag(string tag)
        {
            return _context.Recipes.Where(r => r.TagName == tag).ToList();
        }
        public List<Recipe> GetRecipesByIngredient(string ingredient)
        {
            // Se om du hinner fixa denna, vet inte varför den inte returnerar något.

            return _context.Recipes.Include(r => r.Ingredients).Where(i => i.Name == ingredient).ToList();


            //Fixa denna om tid finns
            // return _context.Recipes.Include(r => r.Ingredients).Where(r => r.Ingredients.FirstOrDefault(i => i.Name == ingredient));

            //.Where(r => r.Ingredients.ForEach(i => i.Name == ingredient));
            // Where(r => r.Ingredients.Contains(ingredient)).ToList();
        }


        public string GetUserNameByRecipeId(int recipeID, UserRepository userRepo)
        {
            return userRepo.FindById(recipeID).UserName; // kraschar ibland? på äggmacka
        }
    }
}
