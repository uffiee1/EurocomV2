using System;
using System.Collections.Generic;
using System.Text;
using EurocomV2_Data;
using EurocomV2_Data.DTO;
using EurocomV2_Model;

namespace EurocomV2_Logic.Container
{
    public class DoctorContainer
    {
        public void UseAddPatientToDoctor(string username, string id)
        {
            DoctorData doctorData = new DoctorData();
            doctorData.AddPatientToDoctor(username, id);
        }

        public AssignModel CallCheckingSecurityCodeMatch(string securityCode)
        {
            DoctorData doctorData = new DoctorData();
            AssignDTO assignDTO = doctorData.CheckSecurityCodeMatch(securityCode);
            AssignModel assignModel;
            if (assignDTO.SecurityCodeMatch)
            {
                assignModel = new AssignModel
                {
                    patientModel = new PatientModel { Id = assignDTO.patientDTO.Id },
                    SecurityCodeMatch = assignDTO.SecurityCodeMatch
                };
            }
            else
            {
                assignModel = new AssignModel
                {
                    SecurityCodeMatch = assignDTO.SecurityCodeMatch
                };
            }
            return assignModel;
        }

        public bool CallCheckRelationDoctorPatient(string username, string id)
        {
            DoctorData doctorData = new DoctorData();
            AssignModel assignModel = new AssignModel
            {
                ExistingRelation = doctorData.CheckExistingRelationDoctorPatient(username, id)
            };
            return assignModel.ExistingRelation;
        }
    }
}
