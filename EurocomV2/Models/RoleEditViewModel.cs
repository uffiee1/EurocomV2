using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
            UserList = new List<string>();
        }


        public string RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }

        public List<string> UserList { get; set; }
    }
}
