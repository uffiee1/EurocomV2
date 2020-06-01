using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models.Classes
{
    public class patient
    {
        public patient(string firstName, string lastName, string number, string username)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
            UserName = username;
        }

        public string FirstName { get; private set;}
        public string LastName { get; private set; }
        public string Number { get; private set; }
        public string UserName { get; private set; }
    }
}
