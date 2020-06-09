using System;
using System.Collections.Generic;
using System.Text;

namespace EurocomV2_Data.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public bool Agreement { get; set; }
        public string Email { get; set; }
    }
}
