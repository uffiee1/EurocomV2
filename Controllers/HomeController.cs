using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EurocomV2.Models;
using System.Data.SqlClient;
using EurocomV2.Models.Classes;
using System.Data;

namespace EurocomV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        SqlConnection sqlConnection = new SqlConnection("server = (LocalDB)\\MSSQLLocalDB; database = EurocomJulian; Trusted_Connection = true; MultipleActiveResultSets = True");
        public IActionResult Status()
        {
            var selectedUser = TempData["SelectedUser"];


            Random rnd = new Random();
            double inrValue = rnd.NextDouble(1.0, 5.0);

            inrValue = Math.Round(inrValue, 1);
            SqlCommand checkAmount = new SqlCommand("CheckAmount", sqlConnection);
            checkAmount.CommandType = CommandType.StoredProcedure;
            SqlParameter amount = new SqlParameter();
            checkAmount.Parameters.AddWithValue("@userID", 1);
            amount.ParameterName = "@amount";
            amount.SqlDbType = SqlDbType.Int;
            amount.Direction = ParameterDirection.Output;
            checkAmount.Parameters.Add(amount);
            sqlConnection.Open();
            checkAmount.ExecuteNonQuery();
            sqlConnection.Close();
            int statusAmount = Convert.ToInt32(amount.Value);
            if (statusAmount > 10)
            {
                SqlCommand selectOldest = new SqlCommand("DELETE FROM PatientStatus WHERE lowest = (SELECT MIN(statusID) from PatientStatus WHERE @userID = 1)");
                sqlConnection.Open();
                sqlConnection.Close();
            }

            SqlCommand updateStatus = new SqlCommand("INSERT INTO  PatientStatus(userID, IR) VALUES(1, @IR)", sqlConnection);
            updateStatus.CommandType = CommandType.Text;

            updateStatus.Parameters.AddWithValue("@IR", inrValue);
            SqlCommand checkStatus = new SqlCommand("CheckStatus", sqlConnection);
            checkStatus.CommandType = CommandType.StoredProcedure;
            checkStatus.Parameters.AddWithValue("@userID", 1);
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

            return View();
        }


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
