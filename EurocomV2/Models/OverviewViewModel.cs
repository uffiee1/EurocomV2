using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class OverviewViewModel
    {
        public PatientViewModel patientViewModel { get; set; }
        public List<PatientViewModel> patientStatus { get; set; }
    }
}
