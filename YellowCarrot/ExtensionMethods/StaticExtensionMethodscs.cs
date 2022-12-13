using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                                          (current,include) => current.Include(include));
            }

            return query;
        }
    }
}
