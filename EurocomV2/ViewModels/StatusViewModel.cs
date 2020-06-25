using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Data_Layer;
using EurocomV2.Models;

namespace EurocomV2.ViewModels
{
    public class StatusViewModel
    {
        public MeasurementDTO Measurement { get; set; }
        public InrDTO InrDto { get; set; }
        public string Status { get; set; }
        public StatusIcon Icon { get; set; }
    }
}
