using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Models;

namespace YellowCarrot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotAppDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //    modelBuilder.Entity<Recipe>().HasData(new Recipe()
            //    {
            //        ID = 1,
            //        UserID = 1,
            //        Name = "Äggmacka Äggus Maximus"

            //    },
            //    new Recipe()
            //    {
            //        ID = 2,
            //        UserID = 2,
            //        Name = "Brända Halsmandlar",
            //    }, new Recipe()
            //    {
            //        ID = 3,
            //        UserID = 3,
            //        Name = "Finsk Sommarsoppa"
            //    });

            //    modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            //    {
            //        ID = 1,
            //        RecipeID= 1,
            //        Name = "Ägg",
            //        Quantity = 2,
            //        Unit="st"

            //    },
            //   new Ingredient()
            //   {
            //       ID = 2,
            //       RecipeID=1,
            //       Name = "Bröd",
            //       Quantity = 1,
            //       Unit="st"

            //   },
            //   new Ingredient()
            //   {
            //       ID = 3,
            //       RecipeID = 1,
            //       Name = "Smör",
            //       Quantity = 1,
            //       Unit = "dl"
            //   },
            //   new Ingredient()
            //   {
            //       ID = 4,
            //       RecipeID = 2,
            //       Name = "Halsmandlar",
            //       Quantity = 2,
            //       Unit = "st"

            //   },
            //   new Ingredient()
            //   {
            //   ID = 5,
            //   RecipeID = 2,
            //   Name = "Eld",
            //   Quantity = 1,
            //   Unit = "st"

            //   },
            //   new Ingredient()
            //   {
            //   ID = 2,
            //   RecipeID = 3,
            //   Name = "Vodka",
            //   Quantity = 1,
            //   Unit = "l"

            //   });
        }
    }

}


