using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models.Classes
{
    public class getInfo
    {


        public char getGenderMethod(ApplicationUser user)
        {
            return user.gender;
        }

        public string getLastName(ApplicationUser user)
        {
            return user.LastName;
        }
    }
}
