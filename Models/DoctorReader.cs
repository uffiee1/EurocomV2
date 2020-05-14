using EurocomV2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EurocomV2.Models
{
    public class DoctorReader
    {
        public List<patient> patients = new List<patient>();
        string PatientIds = "";
        string userIds = "";
        public string test = "";
        public string ConnectionString = "Data Source = mssql.fhict.local; Initial Catalog = dbi406383_eurocom; Persist Security Info = True; User ID = dbi406383_eurocom; Password = Kastanje81;";
        public void Read(int UserId)
        {
            PatientIds = "";
            userIds = "";
            patients.Clear();

            string str = "SELECT DoctorId FROM [Doctor] WHERE UserId = '" + UserId + "';";
            string str2 = "SELECT PatientId FROM [DoctorPatient] WHERE DoctorId = '" + ReadDocterId(ConnectionString, str) + "'";
            test = str2;
           
            ReadPatients(ReadUserIds(ReadPatientIds(ConnectionString, str2), ConnectionString), ConnectionString);
        }

        public string ReadDocterId(string connectionString, string queryString)
        {
            string doctorId = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    doctorId = Convert.ToString(reader.GetInt32("DoctorId"));
                    
                }
                // Call Close when done reading.
                reader.Close();
            }
            return doctorId;
        }

        public List<string> ReadPatientIds(string connectionString, string queryString)
        {
            List<string> patientIds = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    patientIds.Add(Convert.ToString(reader.GetInt32("PatientId")));
                }
                // Call Close when done reading.
                reader.Close();
            }

            return patientIds;
        }

        public List<string> ReadUserIds(List<string> patientIds, string connectionString)
        {
            List<string> UserIds = new List<string>();
            for (int i = 0; i < patientIds.Count; i++)
            {
                if(patientIds.Count == 1 || patientIds.Count - 1 == i)
                {
                    PatientIds += patientIds[i];
                }
                else
                {
                    PatientIds += patientIds[i] + ", ";
                } 
            }

            string queryString = "SELECT UserId FROM [Patient] WHERE PatientId IN (" + PatientIds + ");";
            
            //string queryString = $"SELECT UserId FROM [Patient] WHERE  PatientId = (" + PatientIds + ");";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    UserIds.Add(Convert.ToString(reader.GetInt32("UserId")));
                }
                // Call Close when done reading.
                reader.Close();
            }

            return UserIds;
        }
        public void ReadPatients(List<string> UserIds, string connectionString)
        {
            for (int i = 0; i < UserIds.Count; i++)
            {
                if (UserIds.Count == 1 || UserIds.Count - 1 == i)
                {
                    userIds += UserIds[i];
                }
                else
                {
                    userIds += UserIds[i] + ", ";
                }
            }
            
            string queryString = "SELECT Firstname, Lastname, PhoneNumber, Username FROM [User] WHERE UserId IN (" + userIds + ");";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string FirstName = reader.GetString("Firstname");
                    string LastName = reader.GetString("Lastname");
                    string Number = reader.GetString("PhoneNumber");
                    string UserName = reader.GetString("Username");
                    patient Patient = new patient(FirstName, LastName, Number, UserName);
                    patients.Add(Patient);
                }
                // Call Close when done reading.
                reader.Close();
            }
            
        }


    }
}
