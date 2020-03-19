using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Models;
using EurocomV2.Models.Classes;

namespace EurocomV2.Controllers
{
    public class StatusController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection("server = (LocalDB)\\MSSQLLocalDB; database = EurocomJulian; Trusted_Connection = true; MultipleActiveResultSets = True");
        public IActionResult Status()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Status(StatusViewModel model)
        {
            Random rnd = new Random();
            double inrValue = rnd.NextDouble(1.0, 5.0);

            inrValue = Math.Round(inrValue, 1);

            SqlCommand insertIntoTable = new SqlCommand("UPDATE PatientStatus SET IR = @IR WHERE @userID = userID", sqlConnection);
            insertIntoTable.CommandType = CommandType.Text;
            insertIntoTable.Parameters.AddWithValue("@userID", model.userID);
            insertIntoTable.Parameters.AddWithValue("@IR", inrValue);
            SqlCommand checkStatus = new SqlCommand("CheckStatus", sqlConnection);
            checkStatus.CommandType = CommandType.StoredProcedure;
            checkStatus.Parameters.AddWithValue("@userID", model.userID);
            sqlConnection.Open();
            insertIntoTable.ExecuteNonQuery();
            checkStatus.ExecuteNonQuery();
            sqlConnection.Close();


            if (inrValue > 0 && inrValue < 2)
            {
                ViewBag.Status = "Perfect! Uw INR Waarde is " + inrValue;
            }
            else if (inrValue >= 2 && inrValue <= 3)
            {
                ViewBag.Status = "Goed! Uw INR Waarde is " + inrValue;
            }
            else
            {
                ViewBag.Status = "Ga misschien langs bij uw huisarts, uw INR Waarde is " + inrValue;
            }

            return View();
        }
    }
}
