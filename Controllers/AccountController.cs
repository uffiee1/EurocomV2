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

namespace EurocomV2.Controllers
{

    [Authorize(Roles = Role.Administrator)]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger logger;

        [BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

        public AccountController(UserManager<User> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet]
        public IActionResult Register()
        {
           var RegisterViewModel = new RegisterViewModel
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

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.rememberMe,
                            false);
                    if (result.Succeeded)
                    {
                        string DeviceID = ProcessAPIData.GetClient(await ProcessAPIData.GetAllDevices(), user.Name);
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