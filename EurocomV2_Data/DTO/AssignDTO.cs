using System;
using System.Collections.Generic;
using System.Text;

namespace EurocomV2_Data.DTO
{
    public class AssignDTO
    {
        public PatientDTO patientDTO { get; set; }
        public bool SecurityCodeMatch { get; set; }
        public bool ExistingRelation { get; set; }
    }
}
