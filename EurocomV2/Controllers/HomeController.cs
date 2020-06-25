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
                        measurement.Status = "INR-Waarde te laag! Probeer meer water te drinken.";
                        measurement.Icon = StatusIcon.Slect;
                    }
                    else if (measurement.Measurement.measurementValue >= measurement.InrDto.upperBoundary)
                    {
                        measurement.Status = "INR-Waarde te hoog!  Probeer een wandeling te maken om stress te verminderen.";
                        measurement.Icon = StatusIcon.Slect;
                    }
                    else if (measurement.Measurement.measurementValue > measurement.InrDto.lowerBoundary && measurement.Measurement.measurementValue < measurement.InrDto.upperBoundary)
                    {
                        measurement.Status = "Een prima INR-Waarde! Ga zo door!";
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
                    data.InrDto.client = new ClientDTO()
                    {
                        age = rnd.Next(20, 30),
                        id = nonAPIUser.Id,
                        name = nonAPIUser.Name
                    };
                    data.Measurement = new MeasurementDTO()
                    {
                        deviceID = Guid.NewGuid().ToString(),
                        measurementDate = DateTime.Now,
                        measurementSucceeded = true,
                        measurementTimeInSeconds = 1,
                        measurementValue = Math.Round((decimal)rnd.NextDouble(0.9, 2.1), 2)
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
                        measurementValue = Math.Round((decimal)rnd.NextDouble(0.9, 2.1), 2)
                    };
                    data.InrDto = BoundaryData.GenerateBoundaryData(new ClientDTO
                    {
                        age = rnd.Next(20, 30),
                        id = nonAPIUser.Id,
                        name = nonAPIUser.Name
                    });
                    BoundaryData.InsertBoundaryValues(nonAPIUser.Id, data.InrDto.lowerBoundary, data.InrDto.upperBoundary, data.InrDto.targetValue);
                }

                if (data.Measurement.measurementValue <= data.InrDto.lowerBoundary)
                {
                    data.Status = "INR-Waarde te laag! Probeer meer water te drinken.";
                    data.Icon = StatusIcon.Slect;
                }
                else if (data.Measurement.measurementValue >= data.InrDto.upperBoundary)
                {
                    data.Status = "INR-Waarde te hoog! Probeer een wandeling te maken om stress te verminderen.";
                    data.Icon = StatusIcon.Slect;
                }
                else if (data.Measurement.measurementValue > data.InrDto.lowerBoundary && data.Measurement.measurementValue < data.InrDto.upperBoundary)
                {
                    data.Status = "INR-Waarde prima! Ga zo door!";
                    data.Icon = StatusIcon.Perfect;
                }

                MeasurementData.InsertMeasurement(data.Measurement);
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
            Patient bob = new Patient("Bob", "bobson", "10", "test");
            Patient herman = new Patient("herman", "hermanson", "42", "groen");
            Patient julius = new Patient("julius", "De vries", "42", "groen");
            Patient Tony = new Patient("Tony", "Zhou", "42", "groen");
            Patient Ruud = new Patient("Ruud", "Willems", "42", "groen");
            Patient Julian = new Patient("Julian", "Tekstra", "42", "groen");
            Patient Rens = new Patient("Rens", "van Lieshout", "42", "groen");
            Patient Hendrieka = new Patient("Hendrieka", "Hendriks", "42", "groen");
            PatientsViewModel model = new PatientsViewModel(new List<Patient>
            {
                bob, herman, julius, Tony, Ruud, Julian, Rens, Hendrieka
            });
            return View(model);
        }
    }
}
