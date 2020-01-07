using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BluePope.WorkPortal.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("/api/login")]
        public async Task<IActionResult> Login([FromForm] MLogin input)
        {
            string returnUrl = Url.Content("~/");

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Name, input.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));

            var principal = new ClaimsPrincipal(identity);

            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = false, //로그인 쿠키 영속성 (브라우저 종료시 유지) 여부
                    ExpiresUtc = DateTime.UtcNow.AddDays(7), //7일간 미접속시 쿠키 만료
                    AllowRefresh = true, //갱신여부
                    RedirectUri = this.Request.Host.Value
                });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return Redirect("/");
        }

        [Route("/api/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Redirect("/");
        }
    }

    public class MLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}