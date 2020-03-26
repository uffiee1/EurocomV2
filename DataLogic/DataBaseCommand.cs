using EurocomV2.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EurocomV2.DataLogic
{
    public class DataBaseCommand
    {
        DataBaseConnection dataBaseConnection;
        public DataBaseCommand()
        {
            dataBaseConnection = new DataBaseConnection();
        }

        public bool GetAgreementStatus(string userName)
        {
            dataBaseConnection = new DataBaseConnection();
            bool status = false;
            using (dataBaseConnection.connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("GetAgreement", dataBaseConnection.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", userName);
                    dataBaseConnection.connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                status = dataReader.GetBoolean(0);
                                AgreementDTO agreementDTO = new AgreementDTO()
                                {
                                    Agreement = status
                                };
                            }
                        }
                        dataReader.Close();
                    };
                }

                catch (SqlException ex)
                {
                    string errorMessage = "{0} Service Unavailable: {1} \n Please make sure you have a stable internet connection and try again.";
                    Console.WriteLine(String.Format(errorMessage, "Error 503", ex.ToString()));
                }
                return status;
            }
        }

        public void UpdateAgreementStatus(bool agreed, string userName)
        {
            dataBaseConnection = new DataBaseConnection();
            using (dataBaseConnection.connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UpdateAgreement", dataBaseConnection.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    dataBaseConnection.connection.Open();
                    command.Parameters.AddWithValue("@Agreement", agreed);
                    command.Parameters.AddWithValue("@username", userName);
                    command.ExecuteNonQuery();
                }

                catch (SqlException ex)
                {
                    string errorMessage = "{0} Service Unavailable: {1} \n Please make sure you have a stable internet connection and try again.";
                    Console.WriteLine(String.Format(errorMessage, "Error 503", ex.ToString()));
                }
            }
        }
    }
}
