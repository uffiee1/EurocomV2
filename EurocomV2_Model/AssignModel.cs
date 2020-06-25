using System;
using System.Collections.Generic;
using System.Text;

namespace EurocomV2_Model
{
    public class AssignModel
    {
        public PatientModel patientModel { get; set; }
        public bool SecurityCodeMatch { get; set; }
        public bool ExistingRelation { get; set; }
    }
}
