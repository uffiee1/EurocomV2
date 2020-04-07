using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class UserViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Phonenumber { get; set; }
        public bool Agreement { get; set; }
    }
}
