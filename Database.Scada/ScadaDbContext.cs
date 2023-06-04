using Database.Scada.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Scada
{
    public class ScadaDbContext : DbContext
    {
        public ScadaDbContext()
        {

        }
        public ScadaDbContext(DbContextOptions<ScadaDbContext> options) : base(options)
        {

        }
        
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Contractor> Contractors{ get; set; }

        public virtual DbSet<ProductionOrder> ProductionOrders{ get; set; }

        public virtual DbSet<Skill> Skills{ get; set; }

        public virtual DbSet<StockState> StockStates { get; set; }

        public virtual DbSet<Transaction> Transactions{ get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Role)
                .WithMany(a => a.Employees);

            modelBuilder.Entity<Unit>()
                .HasMany(a => a.Products)
                .WithOne(a => a.Unit);

            modelBuilder.Entity<Address>()
                .HasMany(a => a.Contractors)
                .WithOne(a => a.Address);

            modelBuilder.Entity<Contact>()
                .HasOne(a => a.Contractor)
                .WithMany(a => a.Contacts);

            modelBuilder.Entity<Contractor>()
                .HasMany(a => a.ProductionOrders)
                .WithOne(a => a.Contractor);

            modelBuilder.Entity<Contractor>()
                .HasMany(a => a.Transactions)
                .WithOne(a => a.Contractor);

            modelBuilder.Entity<Product>()
                .HasMany(a => a.ProductionOrders)
                .WithOne(a => a.Product);

            modelBuilder.Entity<Skill>()
                .HasMany(a => a.Employees)
                .WithMany(a => a.Skills)
                .UsingEntity("EmployyesSkills");

            modelBuilder.Entity<StockState>()
                .HasOne(a => a.Product)
                .WithOne(a => a.StockState);
        }
    }
}
