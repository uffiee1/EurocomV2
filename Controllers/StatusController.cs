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
            var selectedUser = TempData["SelectedUser"];


            Random rnd = new Random();
            double inrValue = rnd.NextDouble(1.0, 5.0);

            inrValue = Math.Round(inrValue, 1);
            SqlCommand checkAmount = new SqlCommand("CheckAmount", sqlConnection);
            checkAmount.CommandType = CommandType.StoredProcedure;
            SqlParameter amount = new SqlParameter();
            checkAmount.Parameters.AddWithValue("@userID", selectedUser);
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
                SqlCommand selectOldest = new SqlCommand("DELETE FROM PatientStatus WHERE lowest = (SELECT MIN(statusID) from PatientStatus WHERE @userID = userID)");
                selectOldest.Parameters.AddWithValue("@userID", selectedUser);
                sqlConnection.Open();
                sqlConnection.Close();
            }

            SqlCommand updateStatus = new SqlCommand("INSERT INTO  PatientStatus(userID, IR) VALUES(@userID, @IR)", sqlConnection);
            updateStatus.CommandType = CommandType.Text;
            updateStatus.Parameters.AddWithValue("@userID", selectedUser);
            updateStatus.Parameters.AddWithValue("@IR", inrValue);
            SqlCommand checkStatus = new SqlCommand("CheckStatus", sqlConnection);
            checkStatus.CommandType = CommandType.StoredProcedure;
            checkStatus.Parameters.AddWithValue("@userID", selectedUser);
            sqlConnection.Open();
            updateStatus.ExecuteNonQuery();
            checkStatus.ExecuteNonQuery();
            sqlConnection.Close();
  

            if (inrValue > 0 && inrValue < 2)
            {
                ViewBag.Status = "Perfect! Uw INR Waarde is " + inrValue;
                TempData["Statusicon"] = "Perfect";
            }
            else if (inrValue >= 2 && inrValue <= 3)
            {
                ViewBag.Status = "Goed! Uw INR Waarde is " + inrValue;
                TempData["Statusicon"] = "Goed";
            }
            else
            {
                ViewBag.Status = "Ga misschien langs bij uw huisarts, uw INR Waarde is " + inrValue;
                TempData["Statusicon"] = "Slecht";
            }

            return View();

        }
    }
}
