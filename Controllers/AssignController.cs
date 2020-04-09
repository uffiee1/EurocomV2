using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EurocomV2.Models;
using EurocomV2_Model;
using EurocomV2_Logic.Container;

namespace EurocomV2.Controllers
{
    public class AssignController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AssignStart()
        {
            AssignViewModel assignViewModel = new AssignViewModel
            {
                patients = GetPatientNames()
            };
            return View("Index", assignViewModel);
        }

        public ActionResult AssignClick(string firstname, string lastname)
        {
            DoctorContainer doctorContainer = new DoctorContainer();
            string username = "dTest2";
            doctorContainer.UseAddPatientToDoctor(username, firstname, lastname);
            AssignViewModel assignViewModel = new AssignViewModel
            {
                patients = GetPatientNames(),
                assignSuccess = true,
                assignMessage = "Patient successfully added to doctor!"
            };

            return View("Index", assignViewModel);
        }

        public List<PatientViewModel> GetPatientNames()
        {
            PatientContainer patientContainer = new PatientContainer();
            List<PatientModel> patientsModel = patientContainer.RetreivePatientNames();
            List<PatientViewModel> patients = new List<PatientViewModel>();
            foreach(PatientModel patient in patientsModel)
            {
                PatientViewModel patientViewModel = new PatientViewModel
                {
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname
                };
                patients.Add(patientViewModel);
            }
            return patients;
        }
    }
}