using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cource_work.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace cource_work.Controllers
{
    public class AccountController : Controller
    {
        private bus_stationContext _context;
        public AccountController(bus_stationContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeName");
            ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "RoleName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Euser user = await _context.Euser.FirstOrDefaultAsync(u => u.EuserLogin == model.Login);
                if (user == null)
                {
                    user = new Euser { 
                        EuserLogin = model.Login, 
                        EuserPassword = model.Password,
                        RoleId = model.RoleId,
                        EmployeeId = model.EmployeeId
                    };

                    _context.Euser.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Шось не так");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Euser user = await _context.Euser
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.EuserLogin == model.Login && u.EuserPassword == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Не правильний логін чи пароль");
            }
            return View(model);
        }
        private async Task Authenticate(Euser user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.EuserLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}