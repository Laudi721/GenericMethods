using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Operation> Operations { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Contractor> Contractors { get; set; }

        public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>()
                .HasMany(a => a.Products)
                .WithOne(a => a.Unit);

            modelBuilder.Entity<Contractor>()
                .HasMany(a => a.ProductionOrders)
                .WithOne(a => a.Contractor);

            modelBuilder.Entity<Product>()
                .HasMany(a => a.ProductionOrders)
                .WithOne(a => a.Product);

            modelBuilder.Entity<ProductionOrder>()
                .HasOne(a => a.Contractor)
                .WithMany(a => a.ProductionOrders);

            modelBuilder.Entity<Contractor>()
                .HasMany(a => a.Addresses)
                .WithMany(a => a.Contractors);

            modelBuilder.Entity<Operation>()
                .HasMany(a => a.ProductionOrders)
                .WithMany(a => a.Operations);
        }
    }
}
