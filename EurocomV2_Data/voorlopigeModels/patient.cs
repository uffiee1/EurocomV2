using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2_Data
{
    public class patient
    {
        public patient(string userName, string email, string number)
        {
            UserName = userName;
            Email = email;
            Number = number;
        }

        public string UserName { get; private set;}
        public string Email { get; private set; }
        public string Number { get; private set; }
    }
}
