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

        public async Task<IActionResult> Status()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(id);
                var email = user.Email;
                var userExists = ProcessAPIData.GetClient(await ProcessAPIData.GetAllDevices(), user.Name);
                if (userExists != null)
                {
                    var measurement = new ViewModels.StatusViewModel()
                    {
                        InrDto = await ProcessAPIData.LoadInrData(userExists),
                        Measurement = ProcessAPIData.GetMostRecentDate(await ProcessAPIData.GetMeasurementData(userExists))
                    };
                    if (measurement.Measurement.measurementValue <= measurement.InrDto.lowerBoundary)
                    {
                        measurement.Status = "INR Waarde te laag!";
                        measurement.Icon = StatusIcon.Slect;
                    }
                    else if (measurement.Measurement.measurementValue >= measurement.InrDto.upperBoundary)
                    {
                        measurement.Status = "INR Waarde te hoog!";
                        measurement.Icon = StatusIcon.Slect;
                    }
                    else if (measurement.Measurement.measurementValue > measurement.InrDto.lowerBoundary && measurement.Measurement.measurementValue < measurement.InrDto.upperBoundary)
                    {
                        measurement.Status = "INR Waarde is niet te hoog en ook niet te laag!";
                        measurement.Icon = StatusIcon.Perfect;
                    }
                    return View(measurement);
                }
                ViewModels.StatusViewModel data = new ViewModels.StatusViewModel();
                Random rnd = new Random();
                var nonAPIUser = await _userManager.FindByEmailAsync(email);
                if (BoundaryData.CheckIfBoundaryDataExists(nonAPIUser.Id))
                {
                    data.InrDto = BoundaryData.GetBoundaryData(nonAPIUser.Id);
                    data.Measurement = new MeasurementDTO()
                    {
                        deviceID = Guid.NewGuid().ToString(),
                        measurementDate = DateTime.Now,
                        measurementSucceeded = true,
                        measurementTimeInSeconds = 1,
                        measurementValue = (decimal)1.0
                    };
                }
                else
                {
                    data.Measurement = new MeasurementDTO()
                    {
                        deviceID = Guid.NewGuid().ToString(),
                        measurementDate = DateTime.Now,
                        measurementSucceeded = true,
                        measurementTimeInSeconds = 1,
                        measurementValue = (decimal)1.0
                    };
                    data.InrDto = new InrDTO()
                    {
                        client = new ClientDTO()
                        {
                            age = rnd.Next(20, 30),
                            id = nonAPIUser.Id,
                            name = nonAPIUser.Name
                        },
                        id = Guid.NewGuid().ToString(),
                        lowerBoundary = Math.Round((decimal)rnd.NextDouble(0, 1.0), 2),
                        targetValue = Math.Round((decimal)rnd.NextDouble(1.0, 1.5), 2),
                        upperBoundary = Math.Round((decimal)rnd.NextDouble(1.0, 2.0), 2)
                    };
                    BoundaryData.InsertBoundaryValues(nonAPIUser.Id, data.InrDto.lowerBoundary, data.InrDto.upperBoundary, data.InrDto.targetValue);
                }


                if (data.Measurement.measurementValue <= data.InrDto.lowerBoundary)
                {
                    data.Status = "INR Waarde te laag!";
                    data.Icon = StatusIcon.Slect;
                }
                else if (data.Measurement.measurementValue >= data.InrDto.upperBoundary)
                {
                    data.Status = "INR Waarde te hoog!";
                    data.Icon = StatusIcon.Slect;
                }
                else if (data.Measurement.measurementValue > data.InrDto.lowerBoundary && data.Measurement.measurementValue < data.InrDto.upperBoundary)
                {
                    data.Status = "INR Waarde is niet te hoog en ook niet te laag!";
                    data.Icon = StatusIcon.Perfect;
                }


                return View(data);
            }

            return RedirectToAction("Login", "Account");


        }

        [Route("Home")]
        public async Task<IActionResult> Index()
        {
            TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userManager.FindByIdAsync(TempData["UserID"].ToString());
            return View();
        }

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
            patient bob = new patient("Bob", "bobson", "10", "test");
            patient herman = new patient("herman", "hermanson", "42", "groen");
            patient julius = new patient("julius", "De vries", "42", "groen");
            patient Tony = new patient("Tony", "Zhou", "42", "groen");
            patient Ruud = new patient("Ruud", "Willems", "42", "groen");
            patient Julian = new patient("Julian", "Tekstra", "42", "groen");
            patient Rens = new patient("Rens", "van Lieshout", "42", "groen");
            patient Hendrieka = new patient("Hendrieka", "Hendriks", "42", "groen");
            patientenviewmodel patientenviewmodel = new patientenviewmodel(bob, herman, julius, Tony, Ruud, Julian, Rens, Hendrieka);
            return View(patientenviewmodel);
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
        public IActionResult test()
        {
            patient bob = new patient("Bob", "bobson", "10", "test");
            patient herman = new patient("herman", "hermanson", "42", "groen");
            patient julius = new patient("julius", "De vries", "42", "groen");
            patient Tony = new patient("Tony", "Zhou", "42", "groen");
            patient Ruud = new patient("Ruud", "Willems", "42", "groen");
            patient Julian = new patient("Julian", "Tekstra", "42", "groen");
            patient Rens = new patient("Rens", "van Lieshout", "42", "groen");
            patient Hendrieka = new patient("Hendrieka", "Hendriks", "42", "groen");
            patientenviewmodel patientenviewmodel = new patientenviewmodel(bob, herman, julius, Tony, Ruud, Julian, Rens, Hendrieka);
            return View(patientenviewmodel);
        }
    }
}
