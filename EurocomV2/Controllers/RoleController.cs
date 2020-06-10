using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EurocomV2.Models;
using EurocomV2.Models.Classes;

namespace EurocomV2.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> applicationUser)
        {
            this.roleManager = roleManager;
            this.userManager = applicationUser;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        public IActionResult Index()
        {
            IQueryable<IdentityRole> roles = roleManager.Roles;
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }



        // Role ID is passed from the URL to the action
        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role met Id = {id} kan niet worden gevonden";
                return View("NotFound");
            }

            var model = new RoleEditViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserList.Add(user.UserName);
                }
            }

            return View(model);
        }


        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role met Id = {model.RoleId} kan niet worden gevonden";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }




        [HttpGet]
        public async Task<IActionResult> EditUserRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol met Id = {roleId} kan niet worden gevonden";
                return View("NotFound");
            }

            var model = new List<RoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var roleViewModel = new RoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    roleViewModel.IsSelected = true;
                }
                else
                {
                    roleViewModel.IsSelected = false;
                }

                model.Add(roleViewModel);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<RoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol met Id = {roleId} kan niet worden gevonden";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;
                
                //Also check if the user is already a member of te given role name
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                //If te user is not selected and is in the Role; Remove
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("RoleEdit", new { Id = roleId });
                }
            }
            return RedirectToAction("RoleEdit", new { Id = roleId });
        }
    }
}
















//        private readonly AppDbContext _context;

//        public RoleController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Role
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.RoleViewModel.ToListAsync());
//        }

//        // GET: Role/Details/5
//        public async Task<IActionResult> Details(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var roleViewModel = await _context.RoleViewModel
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (roleViewModel == null)
//            {
//                return NotFound();
//            }

//            return View(roleViewModel);
//        }

//        // GET: Role/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Role/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserId,UserName,IsSelected")] RoleViewModel roleViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(roleViewModel);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }

//            return View(roleViewModel);
//        }

//        // GET: Role/Edit/5
//        public async Task<IActionResult> Edit(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var roleViewModel = await _context.RoleViewModel.FindAsync(id);
//            if (roleViewModel == null)
//            {
//                return NotFound();
//            }

//            return View(roleViewModel);
//        }

//        // POST: Role/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(string id,
//            [Bind("UserId,UserName,IsSelected")] RoleViewModel roleViewModel)
//        {
//            if (id != roleViewModel.UserId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(roleViewModel);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!RoleViewModelExists(roleViewModel.UserId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }

//                return RedirectToAction(nameof(Index));
//            }

//            return View(roleViewModel);
//        }

//        // GET: Role/Delete/5
//        public async Task<IActionResult> Delete(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var roleViewModel = await _context.RoleViewModel
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (roleViewModel == null)
//            {
//                return NotFound();
//            }

//            return View(roleViewModel);
//        }

//        // POST: Role/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(string id)
//        {
//            var roleViewModel = await _context.RoleViewModel.FindAsync(id);
//            _context.RoleViewModel.Remove(roleViewModel);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool RoleViewModelExists(string id)
//        {
//            return _context.RoleViewModel.Any(e => e.UserId == id);
//        }
//    }
//}