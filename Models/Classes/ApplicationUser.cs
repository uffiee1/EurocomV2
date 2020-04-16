using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EurocomV2.Models.Classes
{
    public class ApplicationUser : IdentityUser
    {
        public char gender { get; set; }
    }
}
