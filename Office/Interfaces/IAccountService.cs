using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Office.Interfaces
{
    public interface IAccountService
    {
        public bool LoginUser(LoginDto loginDto);

        public string GenerateJwt(LoginDto loginDto);
    }
}
