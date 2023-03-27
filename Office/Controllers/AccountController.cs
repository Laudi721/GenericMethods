using Dtos.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.Interfaces;

namespace Office.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //[HttpPost]
        //public ActionResult LoginUser([FromBody] LoginDto loginDto)
        //{
        //    return Ok();
        //}

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!_accountService.CheckUser(loginDto))
            {
                return BadRequest("Nieprawidłowy login lub hasło");
            }

            var token = _accountService.GenerateJwt(loginDto);

            return Ok(token);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout(string token)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Ok();
        }
    }
}
