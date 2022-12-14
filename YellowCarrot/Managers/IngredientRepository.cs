using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Interfaces;
using YellowCarrot.Models;

namespace YellowCarrot.Managers
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
            : base(context)
        {
            this._context = context;
        }

        //public void RemoveWhere(Expression<Func<Ingredient, bool>> predicate)
        //{
        //    _context.Remove(predicate);
        //}

    }
}
