using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EurocomV2_Data.DTO;
using Microsoft.Data.SqlClient;

namespace EurocomV2_Data
{
    public class DoctorData
    {
        public void AddPatientToDoctor(string username, string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Doctor_AssignPatient", connectionString.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    if(exception is InvalidCastException || exception is SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
        }

        public AssignDTO CheckSecurityCodeMatch(string securityCode)
        {
            AssignDTO assignDTO = new AssignDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Doctor_CheckSecurityCode", connectionString.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@securityCode", securityCode);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            assignDTO = new AssignDTO
                            {
                                patientDTO = new PatientDTO { Id = reader.GetString(0) },
                                SecurityCodeMatch = true
                            };
                        }
                        else
                        {
                            assignDTO = new AssignDTO
                            {
                                SecurityCodeMatch = false
                            };
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (exception is InvalidCastException || exception is SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
            return assignDTO;
        }

        public bool CheckExistingRelationDoctorPatient(string username, string id)
        {
            AssignDTO assignDTO = new AssignDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Doctor_CheckExistingRelation", connectionString.sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            assignDTO.ExistingRelation = true;
                        }
                        else
                        {
                            assignDTO.ExistingRelation = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (exception is InvalidCastException || exception is SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
            return assignDTO.ExistingRelation;
        }
    }
}
