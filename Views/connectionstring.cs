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
        public List<patient> patients = new List<patient>();
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Doctor> DoctorsPerPatient = new List<Doctor>();

        public void Main(int idMinEen)
        {
            string str = "Data Source = mssql.fhict.local; Initial Catalog = dbi406383_eurocom; Persist Security Info = True; User ID = dbi406383_eurocom; Password = Kastanje81;";
            string str2 = "SELECT Firstname, Lastname, PhoneNumber, Username FROM [User] WHERE Username LIKE 'p%';";
            string str3 = "SELECT Firstname, PhoneNumber FROM [User] WHERE Username LIKE 'd%';";
            ReadData(str, str2);
            ReadData2(str, str3);
            ReadData3(str, idMinEen);

        }
        
        private void ReadData(string connectionString, string queryString)
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
                    patients.Add(Patient);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }

        private void ReadData2(string connectionString, string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string doctor = reader.GetString("Firstname");
                    string numberdoctor = reader.GetString("PhoneNumber");
                    

                    Doctor Doctor1 = new Doctor(doctor, numberdoctor);
                    Doctors.Add(Doctor1);  
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
        private void ReadData3(string connectionString, int idMinEen)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("PatientOverview", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", patients[idMinEen].UserName));

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
