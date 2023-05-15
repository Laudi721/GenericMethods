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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasKey(a => a.Id);
                
            modelBuilder.Entity<Employee>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Role)
                .WithMany(a => a.Employees);

            modelBuilder.Entity<Unit>()
                .HasMany(a => a.Products)
                .WithOne(a => a.Unit);
        }
    }
}
