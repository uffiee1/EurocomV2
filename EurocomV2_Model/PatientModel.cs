using System;
using System.Collections.Generic;
using System.Text;

namespace EurocomV2_Model
{
    public class PatientModel : UserModel
    {
        public StatusModel statusModel { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
    }
}
