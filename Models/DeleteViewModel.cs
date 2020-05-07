using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class DeleteViewModel
    {
        public DoctorViewModel doctorViewModel { get; set; }
        public List<PatientViewModel> patients { get; set; }
        public PatientViewModel patientViewModel { get; set; }
        public string deleteMessage { get; set; }
    }
}
