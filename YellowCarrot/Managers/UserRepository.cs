using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IDisposable // Checka !!! : IUserRepository
    {
        private readonly UserDbContext _Context;
 
        public UserRepository(UserDbContext context)   
        {
            _Context = context;
        }
        public User? FindByUserName(string username) // Filtrering sker via SQL på Databasen 
        {
            return _Context.Users.Where(x => x.UserName == username).FirstOrDefault();
        }
        public User? FindById(int id)
        {
            return _Context.Set<User>().Find(id);
        }
        public IEnumerable<User> FindAll()
        {
            return _Context.Set<User>().ToList();
        }
        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _Context.Set<User>().Where(predicate);
        }
        public void Add(User user)
        {
            _Context.Set<User>().Add(user);
        }
        public void AddRange(IEnumerable<User> users)
        {
            _Context.Set<User>().AddRange(users);
        }
        public void Remove(User user)
        {
            _Context.Set<User>().Remove(user);
        }
        public void RemoveRange(IEnumerable<User> users)
        {
            _Context.Set<User>().RemoveRange(users);
        }


        public int Complete()
        {
            return _Context.SaveChanges();
        }
        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
