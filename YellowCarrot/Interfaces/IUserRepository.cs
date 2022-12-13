using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Interfaces
{
    public interface IUserRepository 
    {
        User FindByUserName(string username);
    }
}
