using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotUserDB;Trusted_Connection=True");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);
        }
    }
}
