using System.Collections.Generic;
using EurocomV2.Models.Classes;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace EurocomV2.ViewModels
{
    public class patientenviewmodel
    {
        public List<patient> patienten { get; set; } = new List<patient>();
        public List<string> Namen { get; set; } = new List<string>();
        public List<string> ids { get; set; } = new List<string>();
        public patientenviewmodel(List<patient> patients)
        {
            this.patienten = patients;
            foreach (patient patient in patienten)
            {
                Namen.Add(patient.UserName);
                ids.Add(patient.Number);
            }
        }
    }
}
