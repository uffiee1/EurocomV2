using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EurocomV2.Models;
using EurocomV2.Models.Classes;
using EurocomV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Data_Layer;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegisterViewModel = EurocomV2.ViewModels.RegisterViewModel;
using LoginViewModel = EurocomV2.ViewModels.LoginViewModel;

namespace EurocomV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //Only Admin & Doctor have permission to register new user
        [HttpGet]
        //[Authorize(Roles = Role.Administrator)]
        //[Authorize(Roles = Role.Doctor)]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                RoleItems = _roleManager.Roles.Select(iR => new SelectListItem
                {
                    Text = iR.Name,
                    Value = iR.Name
                })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.FirstName + model.LastName,
                    Name = model.FirstName + " " + model.LastName,
                    Email = model.Email,
                    gender = model.gender,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!await _roleManager.RoleExistsAsync(Role.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.User));
                }

                if (!await _roleManager.RoleExistsAsync(Role.Administrator))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Role.Administrator));
                }

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["UserID"] = user.Id;
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }

        //Login : GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Login : POST
        //Checks whether the correct combination of the entered email address and passwords are correct
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (ModelState.IsValid)
                {

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.rememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Status", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Email en/of Wachtwoord is incorrect. Probeer het opnieuw.");
                }

            }
            return View(model);
        }

        // Logout button by header section
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        //When user don't have permission to access some pages
        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}