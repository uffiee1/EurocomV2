using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EurocomV2.Views;

namespace EurocomV2
{
    public class connectionstring
    {
        public List<Doctor> DoctorsPerPatient = new List<Doctor>();
        public patient CurrentPatient = new patient("error","error","error","error");
        
        public void Read(int UserId)
        {
            string str = "Data Source = mssql.fhict.local; Initial Catalog = dbi406383_eurocom; Persist Security Info = True; User ID = dbi406383_eurocom; Password = Kastanje81;";
            string str2 = "SELECT Firstname, Lastname, PhoneNumber, Username FROM [User] WHERE Username LIKE 'p%' AND UserId = '" + UserId + "';";
            ReadDataPatient(str, str2);
            ReadDataDoctor(str);
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
                    string LastName2 = reader.GetString("Lastname");
                    string FirstName2 = reader.GetString("Firstname");
                    string number = reader.GetString("PhoneNumber");
                    string UserName = reader.GetString("Username");
                    patient Patient = new patient(FirstName2, LastName2, number, UserName);
                    CurrentPatient = Patient;
                }

                // Call Close when done reading.
                reader.Close();
            }
        }

        // ReadDataDoctor uses a stored procedure to find the doctors that are asigned to this patient
        // and the doctors get added to a list
        private void ReadDataDoctor(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("PatientOverview", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", CurrentPatient.UserName));

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {

                    string Name = reader.GetString(3);
                    string Number = reader.GetString(4);

                    Doctor doctor = new Doctor(Name, Number);
                    DoctorsPerPatient.Add(doctor);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }


    }
}
