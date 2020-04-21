using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Controllers
{
    public class Contact
    {
        public Contact(string firstName, string lastName, string number)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Number { get; private set; }
    }
}
