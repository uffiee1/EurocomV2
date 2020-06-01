/*using System;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Models;
using EurocomV2.Models.Classes;
using EurocomV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Data_Layer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EurocomV2.Controllers
{

    [Authorize(Roles = Role.Administrator)]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger logger;

        [BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

        public AccountController(UserManager<User> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.logger = logger;
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet]
        public IActionResult Register()
        {
          /* var RegisterViewModel = new RegisterViewModel
            {
                RoleItems = roleManager.Roles.Select(iR => new SelectListItem
                {
                    Text = iR.Name,
                    Value = iR.Name
                })
            };
            return View(RegisterViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser() { UserName = model.FirstName + model.LastName, Name = model.FirstName + " " + model.LastName, Email = model.Email, gender = model.gender, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);

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
            return View(model);
        }





        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(user.Username, model.Password, model.rememberMe,
                            false);
                    if (result.Succeeded)
                    {
                        string DeviceID = ProcessAPIData.GetClient(await ProcessAPIData.GetAllDevices(), null);
                        StatusViewModel data = new StatusViewModel()
                        {
                            Measurement =
                                ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(DeviceID)),
                            InrDto = await ProcessAPIData.LoadInrData(DeviceID)
                        };
                        if (data != null)
                        {
                            TempData["Id"] = data.InrDto.id;
                            return RedirectToAction("Status", "Home");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Email/Password combination invalid");
                }
            }

            return View(model);

            }

            public IActionResult Logout()
            {
                _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
        }
    }
*/
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
using MySql.Data.MySqlClient;
using Data_Layer;

namespace EurocomV2.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser() { UserName = model.FirstName + model.LastName, Name = model.FirstName + " " + model.LastName, Email = model.Email, gender = model.gender, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);

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
            return View(model);
        }





        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);

        //        if (user != null)
        //        {
        //            var result =
        //                await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.rememberMe,
        //                    false);
        //            if (result.Succeeded)
        //            {
        //                string DeviceID = ProcessAPIData.GetClient(await ProcessAPIData.GetAllDevices(), user.Name);
        //                StatusViewModel data = new StatusViewModel()
        //                {
        //                    Measurement =
        //                        ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(DeviceID)),
        //                    InrDto = await ProcessAPIData.LoadInrData(DeviceID)
        //                };
        //                if (data != null)
        //                {
        //                    TempData["Id"] = data.InrDto.id;
        //                    return RedirectToAction("Status", "Home");
        //                }
        //            }
        //            ModelState.AddModelError(string.Empty, "Email/Password combination invalid");
        //        }
        //    }

        //    return View(model);

        //}

        //Mockup voor demo
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string DeviceID = ProcessAPIData.GetClient(await ProcessAPIData.GetAllDevices(), "Marjolein Nevensel");
            StatusViewModel data = new StatusViewModel()
            {
                Measurement =
                    ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(DeviceID)),
                InrDto = await ProcessAPIData.LoadInrData(DeviceID)
            };
            if (data != null)
            {
                TempData["Id"] = data.InrDto.id;
                return RedirectToAction("Status", "Home");
            }


            return View(model);

        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}