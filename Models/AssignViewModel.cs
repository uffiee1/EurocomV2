using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class AssignViewModel
    {
        public DoctorViewModel doctorViewModel { get; set; }
        public List<PatientViewModel> patients { get; set; }
        public bool assignSuccess { get; set; }
        public string assignMessage { get; set; }
    }
}
