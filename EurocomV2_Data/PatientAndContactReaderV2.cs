using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EurocomV2_Data
{
    public class PatientAndContactReaderV2
    {
        public List<Doctor> DoctorsPerPatient = new List<Doctor>();
        public List<Contact> Contacts = new List<Contact>();
        public patient CurrentPatient = new patient("error", "error", "error");

        public void Read(string UserId)
        {
            DoctorsPerPatient.Clear();
            Contacts.Clear();
            string str = "Data Source = mssql.fhict.local; Initial Catalog = dbi406383_eurocomv2; Persist Security Info = True; User ID = dbi406383_eurocomV2; Password = Handjeklap1234;";
            string str2 = "SELECT Email, PhoneNumber, UserName FROM [AspNetUsers] WHERE Id = '" + UserId + "';";
            string str3 = "SELECT FirstName, LastName, PhoneNumber FROM [Contacts] WHERE Id = '" + UserId + "';";
            string str4 = "SELECT DoctorId FROM [PatientDoctorLink] WHERE PatientId = '" + UserId + "';";
            ReadDataPatient(str, str2);
            ReadDataContact(str, str3);
            if(ReadDocterids(str, str4).Count > 0)
            {
                ReadDataDocter(ReadDocterids(str, str4), str);
            }
            
        }

        private List<string> ReadDocterids(string connectionString, string queryString)
        {
            List<string> ids = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    ids.Add(reader.GetString("DoctorId"));
                }

                // Call Close when done reading.
                reader.Close();
            }
            return ids;
        }

        private void ReadDataDocter(List<string>dokterIds ,string connectionString)
        {
            string DokterIds = "";
            string queryString = "";
            for (int i = 0; i < dokterIds.Count; i++)
            {
                if (dokterIds.Count == 1 || dokterIds.Count - 1 == i)
                {
                    DokterIds += dokterIds[i];
                }
                else
                {
                    DokterIds += dokterIds[i] + ", ";
                }
            }
            if(dokterIds.Count == 1)
            {
                queryString = "SELECT UserName, PhoneNumber FROM [AspNetUsers] WHERE Id = '" + dokterIds[0] + "';";
            }
            else
            {
                queryString = "SELECT UserName, PhoneNumber FROM [AspNetUsers] WHERE Id IN (" + DokterIds + ");";
            }
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string UserName = reader.GetString("UserName");
                    string number = reader.GetString("PhoneNumber");
                    Doctor doctor = new Doctor(UserName, number);
                    DoctorsPerPatient.Add(doctor);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }

        // ReadDataPatient reads all of the info from the patient and sets that patient as the currentpatient
        private void ReadDataPatient(string connectionString, string queryString)
        {
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
                    CurrentPatient = Patient;
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
        private void ReadDataContact(string connectionString, string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string LastName = reader.GetString("LastName");
                    string FirstName = reader.GetString("FirstName");
                    string number = reader.GetString("PhoneNumber");
                    Contact contact = new Contact(FirstName, LastName, number);
                    Contacts.Add(contact);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
    }
}
