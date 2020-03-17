using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Models;

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
            SqlCommand checkStatus = new SqlCommand("CheckStatus", sqlConnection);
            checkStatus.CommandType = CommandType.StoredProcedure;
            checkStatus.Parameters.AddWithValue("@userID", model.userID);
            SqlParameter IR = new SqlParameter();
            IR.ParameterName = "IR";
            IR.Direction = ParameterDirection.Output;
            IR.SqlDbType = SqlDbType.Int;
            checkStatus.Parameters.Add(IR);
            sqlConnection.Open();
            checkStatus.ExecuteNonQuery();
            int irReader = Convert.ToInt32(IR.Value);
            sqlConnection.Close();

            switch (irReader)
            {
                case 1:
                    ViewBag.Status = "Perfect!";
                    break;
                case 2:
                    ViewBag.Status = "Goed";
                    break;
                case 3:
                    ViewBag.Status = "Goed";
                    break;
                default:
                    ViewBag.Status = "Zie eventueel een huisarts";
                    break;
            }
            return View();
        }
    }
}
