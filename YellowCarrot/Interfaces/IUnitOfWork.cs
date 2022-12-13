using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRecipeRepository Recipes { get; }
        ITagRepository Tags { get; }
        int Complete();
    }
}
