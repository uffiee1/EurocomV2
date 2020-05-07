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
using Data_Layer;
using EurocomV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EurocomV2.Controllers
{
   // [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;



        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        SqlConnection sqlConnection = new SqlConnection("server = (LocalDB)\\MSSQLLocalDB; database = EurocomJulian; Trusted_Connection = true; MultipleActiveResultSets = True");
        public IActionResult Status(InrDTO data)
        {
            var model = new InrDTO()
            {
                id = data.id,
                lowerBoundary = data.lowerBoundary,
                targetValue = data.targetValue,
                upperBoundary = data.upperBoundary
            };

            if (model.targetValue <= model.lowerBoundary)
            {
                ViewBag.Status = "INR Waarde te laag!";
                TempData["StatusIcon"] = "Slecht";
            }
            else if (model.targetValue >= model.upperBoundary)
            {
                ViewBag.Status = "INR Waarde te hoog!";
                TempData["StatusIcon"] = "Slecht";
            }
            else if (model.targetValue > model.lowerBoundary && model.targetValue < model.upperBoundary)
            {
                ViewBag.Status = "INR Waarde is niet te hoog en ook niet te laag!";
                TempData["StatusIcon"] = "Perfect";
            }
            return View(model);
        }

     /*   [Route("Home")]
        public async Task<IActionResult> Index()
        {
            TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userManager.FindByIdAsync(TempData["UserID"].ToString());
            return View();
        }
        */

     public JsonResult Index()
     {
         return Json(ProcessAPIData.LoadInrData("00000bb9-00c8-0000-0000-000000000000"));
    
;     }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Account()
        {
            TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }
        public IActionResult DokterDashboard()
        {
            return View();
        }
        public IActionResult accgegevens()
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
