using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EurocomV2.Models
{
    public class accgegevensModel : PageModel
    {
        private readonly ILogger<accgegevensModel> _logger;

        public accgegevensModel(ILogger<accgegevensModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
