using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EurocomV2.Models;
using EurocomV2.Models.Classes;
using System.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace EurocomV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;



        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        SqlConnection sqlConnection = new SqlConnection("server = (LocalDB)\\MSSQLLocalDB; database = EurocomJulian; Trusted_Connection = true; MultipleActiveResultSets = True");
        public async Task<IActionResult> Status()
        {
            if (_signInManager.IsSignedIn(User))
            {


                var selectedUser = User.FindFirstValue(ClaimTypes.Name);


                Random rnd = new Random();
                double inrValue = rnd.NextDouble(1.0, 5.0);

                inrValue = Math.Round(inrValue, 1);
                SqlCommand updateStatus = new SqlCommand("INSERT INTO  PatientStatus(userID, IR) VALUES(" + "'"+ selectedUser + "'"  +", " + inrValue + ")", sqlConnection);
                updateStatus.CommandType = CommandType.Text;

                SqlCommand checkStatus = new SqlCommand("CheckStatus", sqlConnection);
                checkStatus.CommandType = CommandType.StoredProcedure;
                checkStatus.Parameters.AddWithValue("@userID", selectedUser);
                sqlConnection.Open();
                updateStatus.ExecuteNonQuery();
                checkStatus.ExecuteNonQuery();
                sqlConnection.Close();


                if (inrValue > 0 && inrValue < 2)
                {
                    ViewBag.Status = "Perfect!";
                    TempData["Statusicon"] = "Perfect";
                    TempData["INR"] = inrValue;
                }
                else if (inrValue >= 2 && inrValue <= 3)
                {
                    ViewBag.Status = "Goed!";
                    TempData["Statusicon"] = "Goed";
                    TempData["INR"] = inrValue;
                }
                else
                {
                    ViewBag.Status = "Ga misschien langs bij uw huisarts";
                    TempData["Statusicon"] = "Slecht";
                    TempData["INR"] = inrValue;
                }
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
