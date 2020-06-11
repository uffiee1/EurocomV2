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
    //[Authorize(Roles = Role.Administrator)]
    //[Authorize(Roles = Role.Doctor)]
    public class AccountController : Controller
    {
        /// <summary>
        /// These SignIn field is for logging in and creating users using the identity API
        /// </summary>
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        //[BindProperty] public RegisterViewModel RegisterViewModel { get; set; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        
        /// <summary>
        /// Logout button by header section
        /// </summary>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        //checks whether the correct combination of the entered email address and passwords are correct
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                    
                }
                ModelState.AddModelError(string.Empty, "Email en/of Wachtwoord is incorrect. Probeer het opnieuw.");
            }
            return View(model);
        }

        //[Authorize(Roles = Role.Administrator)]
        //[Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult Register()
        {

            var account = new RegisterViewModel
            {
                RoleItems = roleManager.Roles.Select(iR => new SelectListItem
                {
                    Text = iR.Name,
                    Value = iR.Name
                })
            };
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Email = model.Email, };
                var result = await userManager.CreateAsync((User)user, model.Password);

                if (!await roleManager.RoleExistsAsync(Role.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.User));
                }
                if (!await roleManager.RoleExistsAsync(Role.Administrator))
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.Administrator));
                }

                //IdentityRole identityRole = new IdentityRole
                //{
                //    Name = model.RoleName
                //};

                //result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    if (model.Role == null)
                    {
                        await userManager.AddToRoleAsync(user, Role.User);
                    }

                    await userManager.AddToRoleAsync(user, model.Role);

                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}