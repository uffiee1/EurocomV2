using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models.Classes
{
    public class Comment
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool State { get; set; }
    }
}
