using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EurocomV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EurocomV2.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        //Logout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Login Post
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

                ModelState.AddModelError(string.Empty, "Email/Password combination is incorrect");
            }

            return View(model);
        }

        //Registration Action
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Registration POST action 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User Account)
        {
            bool Status = false;
            string message = "";
            
            // Model Validation 

            if (ModelState.IsValid)
            {

                #region //Email is already Exist 

                var isExist = IsEmailExist(Account.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(Account);
                }
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


                #region Save to Database

                //MySqlConnection conn = new MySqlConnection();
                //MySqlDataAdapter adapter = new MySqlDataAdapter();

                //using (MySqlConnection dc = new MySqlConnection())
                //{
                //    dc.Users.Add(Account);

                //    //Send Email to User
                //    SendVerificationLinkEmail(Account.EmailID, Account.ActivationCode.ToString());
                //    message = "Registration successfully done. Account activation link " + 
                //              " has been sent to your email id:" + Account.EmailID;
                //    Status = true;
                //}
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(Account);
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
        //                  " <br/><br/><a href='"+link+"'>"+link+"</a> ";
        //}

        [NonAction]
        private bool IsEmailExist(string emailID)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }
    }
}