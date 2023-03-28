using Database;
using Database.Models;
using Dtos.Dtos;
using ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Office.Authentication;
using Office.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Office.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(Scada context, IPasswordHasher<Employee> passwordHasher, AuthenticationSettings authenticationSettings) : base(context)
        {
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public bool LoginUser(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwt(LoginDto loginDto)
        {
            var employee = Context.Set<Employee>()
                .Include(a => a.Role)
                .FirstOrDefault(a => a.Login == loginDto.Login);

            if (employee == null)
            {
                return null;
                throw new BadRequestException("Niepoprawny login lub hasło");
            }

            var result = _passwordHasher.VerifyHashedPassword(employee, employee.Password, loginDto.Password);

            if(result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Niepoprawny login lub hasło");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{employee.Login}"),
                new Claim(ClaimTypes.Role, $"{employee.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
