using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public class InrDTO
    {
        public decimal targetValue { get; set; }
        public decimal upperBoundary { get; set; }
        public decimal lowerBoundary { get; set; }
        public string id { get; set; }
        public ClientDTO client { get; set; }
    }
}
