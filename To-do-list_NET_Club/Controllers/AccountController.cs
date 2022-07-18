using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using To_do_list_NET_Club.DataBase;
using To_do_list_NET_Club.Models;
using To_do_list_NET_Club.ViewModels;

namespace To_do_list_NET_Club.Controllers
{
    public class AccountController : Controller
    {
        private NotesContext db;

        public AccountController(NotesContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserModel user = await db.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Notes");
                }

                ModelState.AddModelError(String.Empty, "Incorrect login and(or) password");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserModel user = await db.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (user == null)
                {
                    db.Users.Add(new UserModel
                    {
                        Email = model.Email,
                        Password = model.Password
                    });

                    await db.SaveChangesAsync();

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Notes");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login and(or) password");
                }
            }

            return View();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
