using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class CreateRole
    {
        [Key]
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
