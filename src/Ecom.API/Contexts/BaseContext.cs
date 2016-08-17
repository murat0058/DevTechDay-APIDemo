using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecom.API.Entities;

namespace Ecom.API.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Model Creation
            //I used fluent API for model creation, You can also use Data annotation approach but that is limited in feature. So Fluent API has more capability than data annotation.

            //You can use extension method to this stuff coz in big application this method will be husge and it will be messy.
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasDefaultValue(0.0M);

            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow);
            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
