using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class RoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}
