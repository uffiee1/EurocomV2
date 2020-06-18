using System;
using System.Collections.Generic;
using System.Text;
using EurocomV2_Data;
using EurocomV2_Data.DTO;
using EurocomV2_Model;

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

        public void CallRemovePatientLinkedToDoctor(string username, int userId)
        {
            PatientData patientData = new PatientData();
            patientData.RemovePatientLinkedToDoctor(username, userId);
        }

        public List<PatientModel> RetreivePatientStatus(int userId)
        {
            PatientData patientData = new PatientData();
            List<PatientModel> patientStatus = new List<PatientModel>();
            List<PatientDTO> patientStatusDTO = patientData.GetPatientStatus(userId);
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

        public PatientModel RetreivePatientAdditionalInfo(int userId)
        {
            PatientData patientData = new PatientData();
            PatientDTO patientDTO = patientData.GetPatientAdditionalInfo(userId);
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
