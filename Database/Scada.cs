using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Scada : DbContext
    {
        public Scada(DbContextOptions<Scada> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Role)
                .WithMany(a => a.Employees);
        }
    }
}
