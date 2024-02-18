using BruteForce.Api.Configuration;
using BruteForce.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;

namespace BruteForce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApiConfiguration _configuration;

        public AuthController(IOptions<ApiConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            if (request.Login == _configuration.User.Login && request.Password == _configuration.User.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, request.Login),
                };
                var identity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Ok("Вы вошли в аккаунт!");
            }

            return Ok("Неверный логин или пароль");
        }
    }
}