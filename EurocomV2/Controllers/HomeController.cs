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

        public async Task<IActionResult> Status(StatusViewModel model)
        {
            var id = TempData["Id"];
            var measurementdata = ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(id.ToString()));
            StatusViewModel data = new StatusViewModel();

            if (measurementdata != null)
            {
                data.Measurement = measurementdata;
                data.InrDto = await ProcessAPIData.LoadInrData(id.ToString());
            }
            else
            {
                Random rnd = new Random();
                var user = await _userManager.FindByIdAsync(id.ToString());
                StatusViewModel status = new StatusViewModel()
                {
                    Measurement = new MeasurementDTO()
                    {
                        deviceID = Guid.NewGuid().ToString(),
                        measurementDate = DateTime.Now,
                        measurementSucceeded = true,
                        measurementTimeInSeconds = 1,
                        measurementValue = (decimal)1.0
                    },
                    InrDto = new InrDTO()
                    {
                        client = new ClientDTO()
                        {
                            age = rnd.Next(20, 30),
                            id = user.Id,
                            name = user.Name
                        },
                        id = Guid.NewGuid().ToString(),
                        lowerBoundary = Math.Round((decimal)rnd.NextDouble(1.0, 2.0), 2),
                        targetValue = Math.Round((decimal)rnd.NextDouble(1.0, 2.0), 2),
                        upperBoundary = Math.Round((decimal)rnd.NextDouble(1.0, 2.0), 2)
                    }
                };
            }


            if (data.InrDto.targetValue <= data.InrDto.lowerBoundary)
            {
                data.Status = "INR Waarde te laag!";
                data.Icon = StatusIcon.Slect;
            }
            else if (data.InrDto.targetValue >= data.InrDto.upperBoundary)
            {
                data.Status = "INR Waarde te hoog!";
                data.Icon = StatusIcon.Slect;
            }
            else if (data.InrDto.targetValue > data.InrDto.lowerBoundary && data.InrDto.targetValue < data.InrDto.upperBoundary)
            {
                data.Status = "INR Waarde is niet te hoog en ook niet te laag!";
                data.Icon = StatusIcon.Perfect;
            }
            return View(data);
        }

           [Route("Home")]
           public async Task<IActionResult> Index()
           {
               TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
               await _userManager.FindByIdAsync(TempData["UserID"].ToString());
               return View();
           }
           

    /*    [HttpGet]
        public async Task<List<MeasurementDTO>> Index()
        {
            return await ProcessAPIData.GetMeasurementData("00000bb9-00c8-0000-0000-000000000000");
        }
    */

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Account()
        {
            TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
