using Database.Scada.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Scada
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Contractor> Contractors { get; set; }

        public virtual DbSet<ProductionOrder> ProductionOrders{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Role)
                .WithMany(a => a.Employees);

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
        }
    }
}
