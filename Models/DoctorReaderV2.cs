using EurocomV2.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.Models
{
    public class DoctorReaderV2
    {
        public List<patient> patients = new List<patient>();
        string userIds = "";
        public string Copy = "";
        public string ConnectionString = "Data Source = mssql.fhict.local; Initial Catalog = dbi406383_eurocomv2; Persist Security Info = True; User ID = dbi406383_eurocomV2; Password = Handjeklap1234;";
            
        public void Read(string UserId)
        {
            userIds = "";
            patients.Clear();

            string str2 = "SELECT patientId FROM [PatientDoctorLink] WHERE DoctorId = '" + UserId + "'";
            Copy = str2;


            ReadPatients(ReadPatientIds(ConnectionString, str2), ConnectionString);
        }
        // Reads all patientIds that are linked to the doctorId that was found in ReadDocterId
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
                    patientIds.Add(reader.GetString("patientId"));
                }
                // Call Close when done reading.
                reader.Close();
            }

            return patientIds;
        }
        // Reads all the patients with the UserIds from ReadUserIds
        public void ReadPatients(List<string> UserIds, string connectionString)
        {
            string queryString = "";
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
            if(UserIds.Count == 1)
            {
                queryString = "SELECT UserName, PhoneNumber, Email FROM [AspNetUsers] WHERE Id = '" + userIds + "';";
            }
            else
            {
                queryString = "SELECT UserName, PhoneNumber, Email FROM [AspNetUsers] WHERE Id IN (" + userIds + ");";
            }
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string Name = reader.GetString("UserName");
                    string number = reader.GetString("PhoneNumber");
                    string email = reader.GetString("Email");
                    patient Patient = new patient(Name, email, number);
                    patients.Add(Patient);
                }
                // Call Close when done reading.
                reader.Close();
            }
        }
    }
}
