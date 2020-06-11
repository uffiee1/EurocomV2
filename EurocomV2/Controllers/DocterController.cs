using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EurocomV2.Models;
using EurocomV2_Model;
using EurocomV2_Logic.Container;
using EurocomV2.Resources;

//using ASPNET_MVC_ChartsDemo.Models;
using Newtonsoft.Json;
using EurocomV2_Data;
//using System.Web.Mvc;

namespace EurocomV2.Controllers
{
    public class DoctorController : Controller
    {
        //username v.d. dokter, wordt uiteindelijk verkregen vanuit een andere view.
        string username = "dTest5";

        //userId v.d. patiënt, wordt uiteindelijk meegegeven vanuit een andere view.
        int userId = 1;

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Assign()
        {
            return View();
        }

        public ActionResult Delete()
        {

            return View();
        }

        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult Remove()
        {
            return View();
        }

        public ActionResult Remove_Click()
        {
            return View("Dashboard");
        }

        //public ActionResult Delete_Start()
        //{
        //    DeleteViewModel deleteViewModel = new DeleteViewModel
        //    {
        //        patients = GetPatients(username)
        //    };
        //    return View("Delete", deleteViewModel);
        //}

        public ActionResult Overview_Start()
        {
            OverviewViewModel overviewViewModel = new OverviewViewModel
            {
                patientStatus = GetPatientStatus(username, userId)
            };

            if (overviewViewModel.patientStatus.Count > 0)
            {
                PatientContainer patientContainer = new PatientContainer();
                PatientModel patientModel = patientContainer.RetreivePatientAdditionalInfo(userId);
                overviewViewModel.patientViewModel = new PatientViewModel
                {
                    Firstname = patientModel.Firstname,
                    Lastname = patientModel.Lastname,
                    Phonenumber = patientModel.Phonenumber,
                    Email = patientModel.Email,
                    DateOfBirth = patientModel.DateOfBirth,
                    Age = patientModel.Age
                };
            }

            return View("Overview", overviewViewModel);
        }

        public ActionResult Assign_Click(string securityCode)
        {
            AssignViewModel assignViewModel;
            DoctorContainer doctorContainer = new DoctorContainer();
            AssignModel assignModel = doctorContainer.CallCheckingSecurityCodeMatch(securityCode);
            if (!assignModel.SecurityCodeMatch)
            {
                assignViewModel = new AssignViewModel
                {
                    SecurityCodeMatch = assignModel.SecurityCodeMatch,
                    AssignMessage = Resource.AssignSecurityCodeError
                };
                return View("Assign", assignViewModel);
            }
            else
            {
                assignViewModel = new AssignViewModel
                {
                    patientViewModel = new PatientViewModel { UserId = assignModel.patientModel.UserId },
                    SecurityCodeMatch = assignModel.SecurityCodeMatch
                };
                assignViewModel.ExistingRelation = doctorContainer.CallCheckRelationDoctorPatient(username, assignViewModel.patientViewModel.UserId);
                if (assignViewModel.ExistingRelation)
                {
                    assignViewModel.AssignMessage = Resource.AssignExistingRelation;
                    return View("Assign", assignViewModel);
                }
                else
                {
                    doctorContainer.UseAddPatientToDoctor(username, assignViewModel.patientViewModel.UserId);
                    assignViewModel.AssignMessage = Resource.AssignSuccess;
                    return View("Assign", assignViewModel);
                }
            }
        }

        //public ActionResult Delete_Click(int userId)
        //{
        //    PatientContainer patientContainer = new PatientContainer();
        //    patientContainer.CallRemovePatientLinkedToDoctor(username, userId);

        //    DeleteViewModel deleteViewModel = new DeleteViewModel
        //    {
        //        patients = GetPatients(username),
        //        deleteMessage = Resource.RemoveSuccess
        //    };

        //    return View("Delete", deleteViewModel);
        //}

        public ActionResult Status_RemovePatientFromDoctor()
        {
            PatientContainer patientContainer = new PatientContainer();
            patientContainer.CallRemovePatientLinkedToDoctor(username, userId);

            RemoveViewModel removeViewModel = new RemoveViewModel
            {
                Confirmation = Resource.RemoveSuccess
            };

            return View("Remove", removeViewModel);
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

        public List<PatientViewModel> GetPatientStatus(string username, int userId)
        {
            PatientContainer patientContainer = new PatientContainer();
            List<PatientModel> patientStatusM = patientContainer.RetreivePatientStatus(username, userId);
            List<PatientViewModel> patientStatus = new List<PatientViewModel>();
            foreach (PatientModel patientModel in patientStatusM)
            {
                PatientViewModel patientViewModel = new PatientViewModel
                {
                    statusViewModel = new StatusViewModel
                    {
                        Date = patientModel.statusModel.Date,
                        INR = patientModel.statusModel.INR
                    }
                };
                patientStatus.Add(patientViewModel);
            }
            return patientStatus;
        }

        public ContentResult JSON()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            List<PatientViewModel> patientStatus = GetPatientStatus(username, userId);
            foreach (PatientViewModel status in patientStatus)
            {
                dataPoints.Add(new DataPoint(status.statusViewModel.Date, Convert.ToDouble(status.statusViewModel.INR)));
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
    }
}