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
        public void UseAddPatientToDoctor(string idD, string idP)
        {
            DoctorData doctorData = new DoctorData();
            doctorData.AddPatientToDoctor(idD, idP);
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

        public bool CallCheckRelationDoctorPatient(string idD, string idP)
        {
            DoctorData doctorData = new DoctorData();
            AssignModel assignModel = new AssignModel
            {
                ExistingRelation = doctorData.CheckExistingRelationDoctorPatient(idD, idP)
            };
            return assignModel.ExistingRelation;
        }
    }
}
