using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace EurocomV2.Models
{
    public class RegisterViewModel
    {
        
            [Display(Name = "Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
            public string LastName { get; set; }

            [Display(Name = "Username")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.EmailAddress, ErrorMessage = "Email is niet ingevuld")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password, ErrorMessage = "Incorrent or Missing password")]
            public string Password { get; set; }
        
            public string Role { get; set; }

            public IEnumerable<SelectListItem> RoleList { get; set; }
        public IQueryable<SelectListItem> RoleItems { get; internal set; }
    }
}

