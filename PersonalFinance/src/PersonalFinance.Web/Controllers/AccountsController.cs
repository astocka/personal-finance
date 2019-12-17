using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Data;
using PersonalFinance.Web.ViewModels;

namespace PersonalFinance.Web.Controllers
{
    public class AccountsController : Controller
    {
        protected UserManager<IdentityUser> UserManager { get; }
        protected SignInManager<IdentityUser> SignInManager { get; }
        protected DataContext Context { get; }

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DataContext context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(loginViewModel.Login, loginViewModel.Password, loginViewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
            }

            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(registerViewModel.Login)
                {
                    Email = registerViewModel.Email
                };
                var result = await UserManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await SignInManager.PasswordSignInAsync(registerViewModel.Login, registerViewModel.Password, true, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }

    }
}