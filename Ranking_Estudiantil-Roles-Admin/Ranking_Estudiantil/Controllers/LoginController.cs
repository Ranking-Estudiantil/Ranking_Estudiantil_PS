using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Ranking_Estudiantil.Models;
using Ranking_Estudiantil.Data;

namespace Ranking_Estudiantil.Controllers
{
    public class LoginController : Controller
    {
        private readonly Ranking_Estudiantil.Data.ApplicationDbContext _context;

        public LoginController(Ranking_Estudiantil.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(Person _usuario)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var usuario = _db.ValidarUsuario(_usuario.Email, _usuario.Password);

            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, usuario.FirstName),
                    new Claim("Email",usuario.Email)
                    
                };
                foreach (var rol in usuario.Role.ToString())
                {
                    claims.Add(new Claim(ClaimTypes.Role.ToString(), rol.ToString()));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
