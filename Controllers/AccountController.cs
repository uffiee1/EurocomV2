using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

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
            if (ModelState.IsValid)
            {
                var user = new User { Username = model.Username, Email = model.Email, };
                var result = await userManager.CreateAsync((User)user, model.Password);

                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Role
                };

                result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(Role.User))
                    {
                        await roleManager.CreateAsync(new IdentityRole(Role.User));
                    }

                    if (!await roleManager.RoleExistsAsync(Role.Administrator))
                    {
                        await roleManager.CreateAsync(new IdentityRole(Role.Administrator));
                    }

                    if (model.Role == null)
                    {
                        await userManager.AddToRoleAsync(user, Role.User);
                    }

                    await userManager.AddToRoleAsync(user, model.Role);

                    //await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }














        /// <summary>
        /// Logout button by header section
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                //TempData["User"] = user;
            }
            ModelState.AddModelError(string.Empty, "Email en/of Wachtwoord is incorrect. Probeer het opnieuw.");
            return View(model);
        }
    }

}