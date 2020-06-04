using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EurocomV2.Controllers
{
    public class DocterController : Controller
    {
        public IActionResult Notes()
        {
            return View();
        }
    }
}
