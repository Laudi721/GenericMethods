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

        /// <summary>
        /// Metoda hashująca hasło
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(Employee employee, string password)
        {
            return _passwordHasher.HashPassword(employee, password);
        }

        /// <summary>
        /// Metoda weryfikująca hash
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PasswordVerificationResult VerifyPassword(Employee employee, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(employee, employee.Password, password);

            return result;
        }
    }
}
