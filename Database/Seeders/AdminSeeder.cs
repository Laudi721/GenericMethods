using Database.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database.Seeders
{
    public class AdminSeeder
    {
        private readonly SCADADbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AdminSeeder(SCADADbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void AdminSeed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var role = SeedAdminRole();
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                }
                if (!_context.Users.Any())
                {
                    var user = SeedAdminUser();
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
            }
        }

        private Role SeedAdminRole()
        {
            return new Role
            {
                Name = "Administrator",
            };
        }

        public User SeedAdminUser()
        {
            var password = "0scada_admin0";

            var user = new User
            {
                Login = "admin_scada",
                RoleId = 1,
            };

            user.Password = _passwordHasher.HashPassword(user, password);

            return user;
        }
    }
}
