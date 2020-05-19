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
            /*TempData["LastName"] = model.LastName;
            string firstName;
            string lastName;
        if (ModelState.IsValid)
            {
                if (model.FirstName.Contains(" ") || model.LastName.Contains(" "))
                {
                    firstName = model.FirstName.Replace(" ", "");
                    lastName = model.LastName.Replace(" ", "");
                }

                else
                {
                    firstName = model.FirstName;
                    lastName = model.LastName;
                }
                */
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

    /* [HttpPost]
     public async Task<IActionResult> Login(LoginViewModel model)
     {
         if (ModelState.IsValid)
         {
             var user = await _userManager.FindByEmailAsync(model.Email);
             if (user != null)
             {
                 var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.rememberMe, false);
                 TempData["UserID"] = user.Id;
                 if (result.Succeeded)
                 {
                     return RedirectToAction("Index", "Home");
                 }
             }
             ModelState.AddModelError(string.Empty, "Email/Password combination invalid");
         }

         return View(model);
     }
     */

    [HttpPost]

    /*   public async Task<IActionResult> Login(APILoginTestViewModel model)
       {
           if (ModelState.IsValid)
           {
               string deviceID = ProcessAPIData.GetUserDevice(await ProcessAPIData.GetAllDevices(), model.id);

               StatusViewModel data = new StatusViewModel()
               {
                   Measurement = ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(deviceID)),
                   InrDto = await ProcessAPIData.LoadInrData(deviceID)
               };
               if (data != null)
               {
                   TempData["Id"] = data.InrDto.id;
                   return RedirectToAction("Status", "Home");
               }

           }
           return View(model);
        }
        */

    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            string DeviceID = ProcessAPIData.GetClient(await ProcessAPIData.GetAllDevices(), model.Name);
            StatusViewModel data = new StatusViewModel()
            {
                Measurement = ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(DeviceID)),
                InrDto = await ProcessAPIData.LoadInrData(DeviceID)
            };
            if (data != null)
            {
                TempData["Id"] = data.InrDto.id;
                return RedirectToAction("Status", "Home");
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