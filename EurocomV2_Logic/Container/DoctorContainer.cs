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
        public void UseAddPatientToDoctor(string username, int userId)
        {
            DoctorData doctorData = new DoctorData();
            doctorData.AddPatientToDoctor(username, userId);
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
                    patientModel = new PatientModel { UserId = assignDTO.patientDTO.UserId },
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

        public bool CallCheckRelationDoctorPatient(string username, int userId)
        {
            DoctorData doctorData = new DoctorData();
            AssignModel assignModel = new AssignModel
            {
                ExistingRelation = doctorData.CheckExistingRelationDoctorPatient(username, userId)
            };
            return assignModel.ExistingRelation;
        }
    }
}
