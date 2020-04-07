using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EurocomV2
{
    public class connectionstring
    {
        public List<string> FirstName = new List<string>();
        public List<string> LastName = new List<string>();
        public List<string> Number = new List<string>();
        public List<string> Doctor = new List<string>();
        public List<string> NumberDoctor = new List<string>();

        public void Main()
        {
            string str = "Data Source = mssql.fhict.local; Initial Catalog = dbi406383_eurocom; Persist Security Info = True; User ID = dbi406383_eurocom; Password = Kastanje81;";
            string str2 = "SELECT Firstname, Lastname, PhoneNumber FROM [User] WHERE Username LIKE 'p%';";
            string str3 = "SELECT Firstname, PhoneNumber FROM [User] WHERE Username LIKE 'd%';";
            ReadData(str, str2);
            ReadData2(str, str3);

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
                    
                    FirstName.Add(FirstName2);
                    LastName.Add(LastName2);
                    Number.Add(number);
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
                      
                    
                      Doctor.Add(doctor);
                      NumberDoctor.Add(numberdoctor);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }


    }
}
