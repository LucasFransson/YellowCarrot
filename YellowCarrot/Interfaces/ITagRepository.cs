using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        public Tag GetTagByName(string name);
        List<Tag> GetAllTags();

        public bool IsAlreadyCreated(Tag tag);
    }
}
