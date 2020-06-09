using System;
using System.Collections.Generic;
using System.Text;

namespace EurocomV2_Data.DTO
{
    public class PatientDTO : UserDTO
    {
        public StatusDTO statusDTO { get; set; }

        public string DateOfBirth { get; set; }

        public int Age { get; set; }
    }
}
