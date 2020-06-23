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
using Microsoft.AspNetCore.Identity;

//using ASPNET_MVC_ChartsDemo.Models;
using Newtonsoft.Json;
using EurocomV2_Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
//using System.Web.Mvc;

namespace EurocomV2.Controllers
{
    public class DoctorController : Controller
    {
        public DoctorController(SignInManager<IdentityUser> signInManager)
        {
            this.SignInManager = signInManager;
        }

        public SignInManager<IdentityUser> SignInManager { get; }

        ////Id v.d. dokter, wordt uiteindelijk verkregen vanuit een andere view.
        //string idD = "d34a07a9-0ed0-4765-bbb8-3a4a6cde73b7";

        ////Id v.d. patiënt, wordt uiteindelijk meegegeven vanuit een andere view.
        //string idP = "27f36d8b-6661-4a99-954f-b8c1522852f8";

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

        //not used in project anymore
        //public ActionResult Delete_Start()
        //{
        //    DeleteViewModel deleteViewModel = new DeleteViewModel
        //    {
        //        patients = GetPatients(username)
        //    };
        //    return View("Delete", deleteViewModel);
        //}

        public ActionResult Overview_Start(string ID)
        {
            HttpContext.Session.SetString("patientId", ID);
            OverviewViewModel overviewViewModel = new OverviewViewModel
            {
                patientViewModel = new PatientViewModel(),
                patientStatus = GetPatientStatus(ID)
            };

            if (overviewViewModel.patientStatus.Count > 0)
            {
                PatientContainer patientContainer = new PatientContainer();
                PatientModel patientModel = patientContainer.RetreivePatientAdditionalInfo(ID);
                overviewViewModel.patientViewModel = new PatientViewModel
                {
                    UserId = ID,
                    Firstname = patientModel.Firstname,
                    Lastname = patientModel.Lastname,
                    Phonenumber = patientModel.Phonenumber,
                    Email = patientModel.Email,
                    DateOfBirth = patientModel.DateOfBirth
                };
            }

            return View("Overview", overviewViewModel);
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
                return View("Assign", assignViewModel);
            }
            else
            {
                assignViewModel = new AssignViewModel
                {
                    patientViewModel = new PatientViewModel { UserId = assignModel.patientModel.Id },
                    SecurityCodeMatch = assignModel.SecurityCodeMatch
                };
                string idD = User.FindFirstValue(ClaimTypes.NameIdentifier);
                assignViewModel.ExistingRelation = doctorContainer.CallCheckRelationDoctorPatient(idD, assignViewModel.patientViewModel.UserId);
                if(assignViewModel.ExistingRelation)
                {
                    assignViewModel.AssignMessage = Resource.AssignExistingRelation;
                    return View("Assign", assignViewModel);
                }
                else
                {
                    doctorContainer.UseAddPatientToDoctor(idD, assignViewModel.patientViewModel.UserId);
                    assignViewModel.AssignMessage = Resource.AssignSuccess;
                    return View("Assign", assignViewModel);
                }
            }
        }

        //public ActionResult Delete_Click(int id)
        //{
        //    PatientContainer patientContainer = new PatientContainer();
        //    patientContainer.CallRemovePatientLinkedToDoctor(username, id);

        //    DeleteViewModel deleteViewModel = new DeleteViewModel
        //    {
        //        patients = GetPatients(username),
        //        deleteMessage = Resource.RemoveSuccess
        //    };

        //    return View("Delete", deleteViewModel);
        //}

        public ActionResult Status_RemovePatientFromDoctor(string ID)
        {
            string idD = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PatientContainer patientContainer = new PatientContainer();
            patientContainer.CallRemovePatientLinkedToDoctor(idD, ID);

            RemoveViewModel removeViewModel = new RemoveViewModel
            {
                Confirmation = Resource.RemoveSuccess
            };

            return View("Remove", removeViewModel);
        }

        //not used in project anymore
        //public List<PatientViewModel> GetPatients(string username)
        //{
        //    PatientContainer patientContainer = new PatientContainer();
        //    List<PatientModel> patientsModel = patientContainer.RetreivePatientsLinkedToDoctor(username);
        //    List<PatientViewModel> patients = new List<PatientViewModel>();
        //    foreach (PatientModel patient in patientsModel)
        //    {
        //        PatientViewModel patientViewModel = new PatientViewModel
        //        {
        //            id = patient.id,
        //            Firstname = patient.Firstname,
        //            Lastname = patient.Lastname
        //        };
        //        patients.Add(patientViewModel);
        //    }
        //    return patients;
        //}

        public List<PatientViewModel> GetPatientStatus(string idP)
        {
            PatientContainer patientContainer = new PatientContainer();
            List<PatientModel> patientStatusM = patientContainer.RetreivePatientStatus(idP);
            List<PatientViewModel> patientStatus = new List<PatientViewModel>();
            foreach(PatientModel patientModel in patientStatusM)
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

            string e = HttpContext.Session.GetString("patientId");
            List<PatientViewModel> patientStatus = GetPatientStatus(e);
            foreach (PatientViewModel status in patientStatus)
            {
                dataPoints.Add(new DataPoint(status.statusViewModel.Date, Convert.ToDouble(status.statusViewModel.INR)));
            }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
    }
}