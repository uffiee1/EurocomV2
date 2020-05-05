using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EurocomV2.Controllers
{
    public class AdminNewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}