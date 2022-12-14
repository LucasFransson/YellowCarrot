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
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
            : base(context)
        {
            this._context = context;
        }

        public List<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }
        public Tag GetTagByName(string tagName)
        {
            return _context.Tags.FirstOrDefault(t=>t.Name==tagName);
        }
    }
}
