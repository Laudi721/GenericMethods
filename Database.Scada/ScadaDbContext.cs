﻿using Database.Scada.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Role)
                .WithMany(a => a.Employees);
        }
    }
}