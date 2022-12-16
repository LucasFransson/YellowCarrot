using EntityFrameworkCore.EncryptColumn.Attribute;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Data
{
    public class UserDbContext : DbContext
    {
        private readonly IEncryptionProvider? _provider;
        public UserDbContext()
        {
            _provider = new GenerateEncryptionProvider("1234567asdasdw890112345678921234");
        }

        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotUserDB;Trusted_Connection=True",
                options => options.EnableRetryOnFailure());
        }
               protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);
            modelBuilder.Entity<User>().HasData(new User()
            {
                ID = 1,
                UserName = "user",
                Password = "user",
                FirstName = "Test",
                LastName = "Testsson"

            },
          new User()
          {
              ID = 2,
              UserName = "McDog1337",
              Password = "kodden",
              FirstName = "Kod",
              LastName = "McDog"
          }, new User()
          {
              ID = 3,
              UserName = "DarthTyrannus",
              Password = "order66",
              FirstName = "Emperor",
              LastName = "Palpatine"
          });
        }
    }
}

