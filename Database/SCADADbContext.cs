using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class SCADADbContext : DbContext
    {
        public SCADADbContext(DbContextOptions<SCADADbContext> options) : base(options)
        {            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(x => x.Users);

            modelBuilder.Entity<User>()
                .Property(a => a.Login)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(a => a.Password)
                .IsRequired();
        }
    }
}