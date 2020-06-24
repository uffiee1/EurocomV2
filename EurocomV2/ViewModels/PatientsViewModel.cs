using System.Collections.Generic;
using EurocomV2.Models.Classes;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace EurocomV2.ViewModels
{
    public class PatientsViewModel
    {
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public List<string> Names { get; set; } = new List<string>();
        public List<string> Ids { get; set; } = new List<string>();
        public PatientsViewModel(List<Patient> patients)
        {
            this.Patients = patients;
            foreach (Patient patient in Patients)
            {
                Names.Add(patient.UserName);
                Ids.Add(patient.Number);
            }
        }
    }
}
