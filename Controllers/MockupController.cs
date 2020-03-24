using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using EurocomV2.Models.TestViewModels;

namespace EurocomV2.Controllers
{
    public class MockupController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginTestViewModel model)
        {
            if (ModelState.IsValid)
            {
                SqlConnection sqlConnection = new SqlConnection("server = (LocalDB)\\MSSQLLocalDB; database = EurocomJulian; Trusted_Connection = true; MultipleActiveResultSets = True");
                SqlCommand login = new SqlCommand("login", sqlConnection);
                login.CommandType = CommandType.StoredProcedure;
                login.Parameters.AddWithValue("@naam", model.name);
                login.Parameters.AddWithValue("@ww", model.password);
                SqlParameter oblogin = new SqlParameter();
                oblogin.ParameterName = "isValid";
                oblogin.SqlDbType = SqlDbType.Bit;
                oblogin.Direction = ParameterDirection.Output;
                login.Parameters.Add(oblogin);
                sqlConnection.Open();
                login.ExecuteNonQuery();

                int res = Convert.ToInt32(oblogin.Value);
                if (res == 1)
                {
                    SqlCommand selectUser = new SqlCommand("SELECT userID from Users WHERE @ww = wachtwoord AND @naam = naam", sqlConnection);
                    selectUser.CommandType = CommandType.Text;
                    selectUser.Parameters.AddWithValue("@naam", model.name);
                    selectUser.Parameters.AddWithValue("@ww", model.password);

                    SqlDataReader balReader = selectUser.ExecuteReader();
                    if (balReader.Read())
                    {
                        int selectedUser = (int)balReader["userID"];
                        sqlConnection.Close();
                        TempData["SelectedUser"] = selectedUser;
                        return RedirectToAction("Status", "Status");
                    }
                }
            }
            return View();
        }
    }
}
