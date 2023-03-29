using Database.Scada.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Scada.Seeders
{
    public class AdminSeeder
    {
        private readonly ScadaDbContext _context;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public AdminSeeder(ScadaDbContext context, IPasswordHasher<Employee> passwordHasher)
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
                if (!_context.Employees.Any())
                {
                    var user = SeedAdminUser();
                    _context.Employees.Add(user);
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

        public Employee SeedAdminUser()
        {
            var password = "0scada_admin0";

            var user = new Employee
            {
                Login = "admin_scada",
                RoleId = 1,
            };

            user.Password = _passwordHasher.HashPassword(user, password);

            return user;
        }
    }
}
