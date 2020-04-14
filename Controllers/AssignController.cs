using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EurocomV2.Models;
using EurocomV2_Model;
using EurocomV2_Logic.Container;
using EurocomV2.Resources;

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

        public ActionResult AssignClick(int userId)
        {
            DoctorContainer doctorContainer = new DoctorContainer();
            //string username wordt uiteindelijk meegegeven, voor nu is het een test string.
            string username = "dTest4";
            AssignViewModel assignViewModel = new AssignViewModel
            {
                doctorViewModel = new DoctorViewModel { ExistingRelation = doctorContainer.CallCheckRelationDoctorPatient(username, userId) },
                patients = GetPatientNames()
            };

            if(assignViewModel.doctorViewModel.ExistingRelation)
            {
                assignViewModel.assignMessage = Resource.AssignExistingRelation;
            }
            else
            {
                doctorContainer.UseAddPatientToDoctor(username, userId);
                assignViewModel.assignMessage = Resource.AssignSuccess;
            }

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
                    UserId = patient.UserId,
                    Firstname = patient.Firstname,
                    Lastname = patient.Lastname
                };
                patients.Add(patientViewModel);
            }
            return patients;
        }
    }
}