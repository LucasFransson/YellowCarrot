using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Managers;
using YellowCarrot.Models;

namespace YellowCarrot.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {

        //void RemoveWhere(Expression<Func<Ingredient, bool>> predicate);
    }
}
