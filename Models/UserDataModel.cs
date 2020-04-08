using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2
{
    /// <summary>
    /// Our User Database table representational model
    /// </summary>
    public class UserDataModel
    {
        public int UserID { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
