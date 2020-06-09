using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models.Classes
{
    public class Doctor
    {
        public Doctor(string name, string number)
        {
            Name = name;
            Number = number;
            
        }

        public string Name { get; private set; }
        public string Number { get; private set; }
        
    }
}
