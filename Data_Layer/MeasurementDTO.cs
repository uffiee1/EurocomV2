using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public class MeasurementDTO
    {
        public string deviceID { get; set; }
        public DateTime measurementDate { get; set; }
        public bool measurementSucceeded { get; set; }
        public int measurementTimeInSeconds { get; set; }
        public decimal? measurementValue { get; set; }
    }
}
