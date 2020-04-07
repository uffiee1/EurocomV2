using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2
{
    public class dbConnection
    {
        public dbConnection(string value) => Value = value;

        public string Value { get; }
    }
}
