using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EurocomV2_Logic
{
    public class PatientLogic
    {
        Random random = new Random();
        public string RandomCode(int codeLength)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, codeLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
