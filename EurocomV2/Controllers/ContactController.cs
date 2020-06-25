using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2_Logic;
using Microsoft.AspNetCore.Mvc;

namespace EurocomV2.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Mail()
        {
            SendMail sendMail = new SendMail();
            string succeeded = sendMail.GetMailServerConnection();
            return View();
        }
    }
}
