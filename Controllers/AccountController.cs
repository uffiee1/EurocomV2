using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EurocomV2.Models;
using EurocomV2.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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

        //Login
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            TempData["LastName"] = model.LastName;
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
                var user = new ApplicationUser() { UserName = Guid.NewGuid() + "_" + firstName + lastName, Email = model.Email, gender = model.gender };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    ViewBag.Gender = user.gender;
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
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
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.rememberMe, false);
                TempData["User"] = user;

                #region //Email is already Exist 

                //var isExist = IsEmailExist(Account.EmailID);
                //if (isExist)
                //{
                //    ModelState.AddModelError("EmailExist", "Email already exist");
                //    return View(Account);
                //}
                #endregion

                //[Could Have] Activation Code voor nieuwe Gebruikers
                #region Generate Activation Code 
                //Account.ActivationCode = Guid.NewGuid();
                #endregion

                //Password Hashing
                #region  Password Hashing 
                Account.Password = Crypto.Hash(Account.Password);
                Account.ConfirmPassword = Crypto.Hash(Account.ConfirmPassword); //
                #endregion
                Account.IsEmailVerified = false;


                //#region Save to Database

                //MySqlConnection conn = new MySqlConnection();
                //MySqlDataAdapter adapter = new MySqlDataAdapter();

                //using (MySqlConnection dc = new MySqlConnection())
                //{
                //    dc.User.Add(Account);

                //    //Send Email to User
                //    SendVerificationLinkEmail(Account.EmailID, Account.ActivationCode.ToString());
                //    message = "Registration successfully done. Account activation link " +
                //              " has been sent to your email id:" + Account.EmailID;
                //    Status = true;
                //}
                //#endregion
            }

        ///Deze Featire werkt nog niet//
        //[Could Have]
        //[NonAction]
        //private void SendVerificationLinkEmail(string emailID, string activationCode)
        //{
        //    var verifyUrl = "/User/VerifyAccount/" + activationCode;
        //    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        //    var fromEmail = new MailAddress("u.angay@gmail.com", "Ufuk Angay - Admin");
        //    var toEmail = new MailAddress(emailID);
        //    var fromEmailPassword = "********"; // Replace with actual password
        //    string subject = "Your account is successfully created!";

        //    string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
        //                  " successfully created. Please click on the below link to verify your account" +
        //                  " <br/><br/><a href='" + link + "'>" + link + "</a> ";
        //}

        //[NonAction]
        //private bool IsEmailExist(string emailID)
        //{
        //    using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    {
        //        var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
        //        return v != null;
        //    }
        //}
    }
}