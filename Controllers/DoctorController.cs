using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EurocomV2.Models;
using EurocomV2_Model;
using EurocomV2_Logic;
using EurocomV2_Logic.Container;
using EurocomV2.Resources;

namespace EurocomV2.Controllers
{
    public class DoctorController : Controller
    {
        //test string, wordt uiteindelijk verkregen vanuit een ander form.
        string username = "dTest5";

        public ActionResult Assign()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Delete_Start()
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel
            {
                patients = GetPatients(username)
            };
            return View("Delete", deleteViewModel);
        }

        public ActionResult Assign_Click(string securityCode)
        {
            AssignViewModel assignViewModel;
            DoctorContainer doctorContainer = new DoctorContainer();
            AssignModel assignModel = doctorContainer.CallCheckingSecurityCodeMatch(securityCode);
            if(!assignModel.SecurityCodeMatch)
            {
                assignViewModel = new AssignViewModel
                {
                    SecurityCodeMatch = assignModel.SecurityCodeMatch,
                    AssignMessage = Resource.AssignSecurityCodeError
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
                    assignViewModel.AssignMessage = Resource.AssignExistingRelation;
                    return View("Index", assignViewModel);
                }
                else
                {
                    doctorContainer.UseAddPatientToDoctor(username, assignViewModel.patientViewModel.UserId);
                    assignViewModel.AssignMessage = Resource.AssignSuccess;
                    return View("Index", assignViewModel);
                }
            }
        }

        public ActionResult Delete_Click(int userId)
        {
            PatientContainer patientContainer = new PatientContainer();
            patientContainer.CallRemovePatientLinkedToDoctor(username, userId);

            DeleteViewModel deleteViewModel = new DeleteViewModel
            {
                patients = GetPatients(username),
                deleteMessage = Resource.DeleteSuccess
            };

            return View("Delete", deleteViewModel);
        }

        public List<PatientViewModel> GetPatients(string username)
        {
            PatientContainer patientContainer = new PatientContainer();
            List<PatientModel> patientsModel = patientContainer.RetreivePatientsLinkedToDoctor(username);
            List<PatientViewModel> patients = new List<PatientViewModel>();
            foreach (PatientModel patient in patientsModel)
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