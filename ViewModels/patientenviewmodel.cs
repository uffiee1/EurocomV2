using System.Collections.Generic;
using EurocomV2.Models.Classes;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace EurocomV2.ViewModels
{
    public class patientenviewmodel
    {
        public List<patient> patienten { get; set; }
        public List<string> Namen { get; set; }
        public List<int> ids { get; set; }
        public patientenviewmodel(patient bob, patient herman)
        {
            patienten = new List<patient>();
            Namen = new List<string>();
            ids = new List<int>();
            ids.Add(42);
            ids.Add(2012);
            patienten.Add(bob);
            patienten.Add(herman);
            foreach (patient patient in patienten)
            {
                Namen.Add(patient.FirstName + " " + patient.LastName);
            }
        }
    }
}
