using Database.Scada.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Base.StaticMethod
{
    public static class HashHelper
    {
        private static readonly IPasswordHasher<Employee> _passwordHasher;

        static HashHelper()
        {
            _passwordHasher = new PasswordHasher<Employee>();
        }

        public static string HashPassword(Employee employee, string password)
        {
            return _passwordHasher.HashPassword(employee, password);
        }

        public static PasswordVerificationResult VerifyPassword(Employee employee, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(employee, employee.Password, password);

            return result;
        }
    }
}
