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

        //public ActionResult AssignStart()
        //{
        //    AssignViewModel assignViewModel = new AssignViewModel
        //    {
        //        patients = GetPatientNames()
        //    };
        //    return View("Index", assignViewModel);
        //}

        public ActionResult Assign_Click(string securityCode)
        {
            //string username wordt uiteindelijk meegegeven, voor test doeleinden is deze lokaal aangemaakt.
            string username = "dTest3";
            AssignViewModel assignViewModel;
            DoctorContainer doctorContainer = new DoctorContainer();
            AssignModel assignModel = doctorContainer.CallCheckingSecurityCodeMatch(securityCode);
            if(!assignModel.SecurityCodeMatch)
            {
                assignViewModel = new AssignViewModel
                {
                    SecurityCodeMatch = assignModel.SecurityCodeMatch,
                    assignMessage = Resource.AssignSecurityCodeError
                };
                return View("Index", assignViewModel);
            }
            else
            {
                assignViewModel = new AssignViewModel
                {
                    patientViewModel = new PatientViewModel { UserId = assignModel.patientModel.UserId },
                    SecurityCodeMatch = assignModel.SecurityCodeMatch
                };
                assignViewModel.ExistingRelation = doctorContainer.CallCheckRelationDoctorPatient(username, assignViewModel.patientViewModel.UserId);
                if(assignViewModel.ExistingRelation)
                {
                    assignViewModel.assignMessage = Resource.AssignExistingRelation;
                    return View("Index", assignViewModel);
                }
                else
                {
                    doctorContainer.UseAddPatientToDoctor(username, assignViewModel.patientViewModel.UserId);
                    assignViewModel.assignMessage = Resource.AssignSuccess;
                    return View("Index", assignViewModel);
                }
            }
        }

        //public ActionResult AssignClick(int userId)
        //{
        //    DoctorContainer doctorContainer = new DoctorContainer();
        //    //string username wordt uiteindelijk meegegeven, voor nu is het een test string.
        //    string username = "dTest4";
        //    AssignViewModel assignViewModel = new AssignViewModel
        //    {
        //        doctorViewModel = new DoctorViewModel { ExistingRelation = doctorContainer.CallCheckRelationDoctorPatient(username, userId) },
        //        patients = GetPatientNames()
        //    };

        //    if(assignViewModel.doctorViewModel.ExistingRelation)
        //    {
        //        assignViewModel.assignMessage = Resource.AssignExistingRelation;
        //    }
        //    else
        //    {
        //        doctorContainer.UseAddPatientToDoctor(username, userId);
        //        assignViewModel.assignMessage = Resource.AssignSuccess;
        //    }

        //    return View("Index", assignViewModel);
        //}

        //public List<PatientViewModel> GetPatientNames()
        //{
        //    PatientContainer patientContainer = new PatientContainer();
        //    List<PatientModel> patientsModel = patientContainer.RetreivePatientNames();
        //    List<PatientViewModel> patients = new List<PatientViewModel>();
        //    foreach(PatientModel patient in patientsModel)
        //    {
        //        PatientViewModel patientViewModel = new PatientViewModel
        //        {
        //            UserId = patient.UserId,
        //            Firstname = patient.Firstname,
        //            Lastname = patient.Lastname
        //        };
        //        patients.Add(patientViewModel);
        //    }
        //    return patients;
        //}
    }
}