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

//using ASPNET_MVC_ChartsDemo.Models;
using Newtonsoft.Json;
//using System.Web.Mvc;

namespace EurocomV2.Controllers
{
    public class DoctorController : Controller
    {
        //test string v.d. dokter, wordt uiteindelijk verkregen vanuit een andere form.
        string username = "dTest5";

        //test id v.d. patiënt, wordt uiteindelijk meegegeven vanuit een andere view.
        int userId = 6;

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

        public ActionResult Delete_Start()
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel
            {
                patients = GetPatients(username)
            };
            return View("Delete", deleteViewModel);
        }

        public ActionResult Overview_Start()
        {
            OverviewViewModel overviewViewModel = new OverviewViewModel
            {
                patientStatus = GetPatientStatus(username, userId)
            };

            if (overviewViewModel.patientStatus.Count > 0)
            {
                overviewViewModel.patientViewModel = overviewViewModel.patientStatus[0];
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

        public List<PatientViewModel> GetPatientStatus(string username, int userId)
        {
            PatientContainer patientContainer = new PatientContainer();
            List<PatientModel> patientStatusM = patientContainer.RetreivePatientStatus(username, userId);
            List<PatientViewModel> patientStatus = new List<PatientViewModel>();
            foreach(PatientModel patientModel in patientStatusM)
            {
                PatientViewModel patientViewModel = new PatientViewModel
                {
                    Firstname = patientModel.Firstname,
                    Lastname = patientModel.Lastname,
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

            //dataPoints.Add(new DataPoint(1481999400000, 4.67));
            //dataPoints.Add(new DataPoint(1482604200000, 4.7));
            //dataPoints.Add(new DataPoint(1483209000000, 4.96));
            //dataPoints.Add(new DataPoint(1483813800000, 5.12));
            //dataPoints.Add(new DataPoint(1484418600000, 5.08));
            //dataPoints.Add(new DataPoint(1485023400000, 5.11));
            //dataPoints.Add(new DataPoint(1485628200000, 5));
            //dataPoints.Add(new DataPoint(1486233000000, 5.2));
            //dataPoints.Add(new DataPoint(1486837800000, 4.7));
            //dataPoints.Add(new DataPoint(1487442600000, 4.74));
            //dataPoints.Add(new DataPoint(1488047400000, 4.67));
            //dataPoints.Add(new DataPoint(1488652200000, 4.66));
            //dataPoints.Add(new DataPoint(1489257000000, 4.86));
            //dataPoints.Add(new DataPoint(1489861800000, 4.91));
            //dataPoints.Add(new DataPoint(1490466600000, 5.12));
            //dataPoints.Add(new DataPoint(1491071400000, 5.4));
            //dataPoints.Add(new DataPoint(1491676200000, 5.08));
            //dataPoints.Add(new DataPoint(1492281000000, 5.05));
            //dataPoints.Add(new DataPoint(1492885800000, 4.98));
            //dataPoints.Add(new DataPoint(1493490600000, 4.89));
            //dataPoints.Add(new DataPoint(1494095400000, 4.9));
            //dataPoints.Add(new DataPoint(1494700200000, 4.95));
            //dataPoints.Add(new DataPoint(1495305000000, 4.88));
            //dataPoints.Add(new DataPoint(1495909800000, 5.07));
            //dataPoints.Add(new DataPoint(1496514600000, 5.14));
            //dataPoints.Add(new DataPoint(1497119400000, 5.05));
            //dataPoints.Add(new DataPoint(1497724200000, 5.03));
            //dataPoints.Add(new DataPoint(1498329000000, 4.93));
            //dataPoints.Add(new DataPoint(1498933800000, 4.97));
            //dataPoints.Add(new DataPoint(1499538600000, 4.86));
            //dataPoints.Add(new DataPoint(1500143400000, 4.95));
            //dataPoints.Add(new DataPoint(1500748200000, 4.83));
            //dataPoints.Add(new DataPoint(1501353000000, 4.83));
            //dataPoints.Add(new DataPoint(1501957800000, 4.73));
            //dataPoints.Add(new DataPoint(1502562600000, 4.56));
            //dataPoints.Add(new DataPoint(1503167400000, 4.34));
            //dataPoints.Add(new DataPoint(1503772200000, 4.25));
            //dataPoints.Add(new DataPoint(1504377000000, 4.18));
            //dataPoints.Add(new DataPoint(1504981800000, 4.22));
            //dataPoints.Add(new DataPoint(1505586600000, 4.18));
            //dataPoints.Add(new DataPoint(1506191400000, 4.31));
            //dataPoints.Add(new DataPoint(1506796200000, 4.34));
            //dataPoints.Add(new DataPoint(1507401000000, 4.47));
            //dataPoints.Add(new DataPoint(1508005800000, 4.57));
            //dataPoints.Add(new DataPoint(1508610600000, 4.63));
            //dataPoints.Add(new DataPoint(1509215400000, 4.55));
            //dataPoints.Add(new DataPoint(1509820200000, 4.55));
            //dataPoints.Add(new DataPoint(1510425000000, 4.44));
            //dataPoints.Add(new DataPoint(1511029800000, 4.46));
            //dataPoints.Add(new DataPoint(1511634600000, 4.41));
            //dataPoints.Add(new DataPoint(1512239400000, 4.3));
            //dataPoints.Add(new DataPoint(1512844200000, 4.31));
            //dataPoints.Add(new DataPoint(1513449000000, 4.3));
            //dataPoints.Add(new DataPoint(1513621800000, 4.36));


            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
    }
}