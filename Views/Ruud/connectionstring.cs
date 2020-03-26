using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Dashboard
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
            string str = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=testDataGegevens; Integrated Security = True";   
            ReadData(str);
        }

        private void ReadData(string connectionString)
        {
            string queryString =
                "SELECT FirstName, LastName, PhoneNumber, Doctor, PhoneNumberDoctor FROM dbo.tbl_test2;";

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    string LastName2 = reader.GetString("LastName");
                    string FirstName2 = reader.GetString("FirstName");
                    string number = reader.GetString("PhoneNumber");
                    string doctor = reader.GetString("Doctor");
                    string numberdoctor = reader.GetString("PhoneNumberDoctor");

                    FirstName.Add(FirstName2);
                    LastName.Add(LastName2);
                    Number.Add(number);
                    Doctor.Add(doctor);
                    NumberDoctor.Add(numberdoctor);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }

        
    }
}
