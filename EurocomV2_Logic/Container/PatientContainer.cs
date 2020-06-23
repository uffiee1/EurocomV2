using System;
using System.Collections.Generic;
using System.Text;
using EurocomV2_Model;
using EurocomV2_Data;
using EurocomV2_Data.DTO;

namespace EurocomV2_Logic.Container
{
    public class PatientContainer
    {
        //not used anymore in project
        //public List<PatientModel> RetreivePatientsLinkedToDoctor(string username)
        //{
        //    PatientData patientData = new PatientData();
        //    List<PatientDTO> patientsDTO = patientData.GetPatientsLinkedToDoctor(username);
        //    List<PatientModel> patients = new List<PatientModel>();
        //    foreach(PatientDTO patient in patientsDTO)
        //    {
        //        PatientModel patientModel = new PatientModel
        //        {
        //            UserId = patient.UserId,
        //            Firstname = patient.Firstname,
        //            Lastname = patient.Lastname
        //        };
        //        patients.Add(patientModel);
        //    }
        //    return patients;
        //}

        public void CallRemovePatientLinkedToDoctor(string idD, string idP)
        {
            PatientData patientData = new PatientData();
            patientData.RemovePatientLinkedToDoctor(idD, idP);
        }

        public List<PatientModel> RetreivePatientStatus(string idP)
        {
            PatientData patientData = new PatientData();
            List<PatientModel> patientStatus = new List<PatientModel>();
            List<PatientDTO> patientStatusDTO = patientData.GetPatientStatus(idP);
            foreach(PatientDTO status in patientStatusDTO)
            {
                PatientModel patientModel = new PatientModel
                {
                    statusModel = new StatusModel
                    {
                        Date = status.statusDTO.Date,
                        INR = status.statusDTO.INR
                    }
                };
                patientStatus.Add(patientModel);
            }
            return patientStatus;
        }

        public PatientModel RetreivePatientAdditionalInfo(string idP)
        {
            PatientData patientData = new PatientData();
            PatientDTO patientDTO = patientData.GetPatientAdditionalInfo(idP);
            PatientModel patientModel = new PatientModel
            {
                Firstname = patientDTO.Firstname,
                Lastname = patientDTO.Lastname,
                Phonenumber = patientDTO.Phonenumber,
                Email = patientDTO.Email,
                DateOfBirth = patientDTO.DateOfBirth
            };
            return patientModel;
        }
    }
}
