using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EurocomV2_Data.DTO;
using Microsoft.Data.SqlClient;

namespace EurocomV2_Data
{
    public class PatientData
    {
        //not used in project anymore
        //public List<PatientDTO> GetPatientsLinkedToDoctor(string username)
        //{
        //    List<PatientDTO> patients = new List<PatientDTO>();
        //    using (ConnectionString connectionString = new ConnectionString())
        //    {
        //        try
        //        {
        //            connectionString.sqlConnection.Open();
        //            SqlCommand cmd = new SqlCommand("GetPatientsLinkedToDoctor", connectionString.sqlConnection);
        //            cmd.Parameters.AddWithValue("username", username);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if(reader.HasRows)
        //                {
        //                    while(reader.Read())
        //                    {
        //                        PatientDTO patientDTO = new PatientDTO
        //                        {
        //                            UserId = reader.GetInt32(0),
        //                            Firstname = reader.GetString(1),
        //                            Lastname = reader.GetString(2)
        //                        };
        //                        patients.Add(patientDTO);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            if (exception is InvalidCastException || exception is SqlException)
        //            {
        //                Console.WriteLine("Error source: " + exception);
        //                throw;
        //            }
        //        }
        //    }
        //    return patients;
        //}

        public void RemovePatientLinkedToDoctor(string idD, string idP)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_Doctor_RemovePatient", connectionString.sqlConnection);
                    cmd.Parameters.AddWithValue("@idD", idD);
                    cmd.Parameters.AddWithValue("@idP", idP);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    if (exception is InvalidCastException || exception is Microsoft.Data.SqlClient.SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
        }

        public List<PatientDTO> GetPatientStatus(string idP)
        {
            List<PatientDTO> patientStatus = new List<PatientDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_Doctor_GetMeasurements", connectionString.sqlConnection);
                    cmd.Parameters.AddWithValue("@idP", idP);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PatientDTO patientDTO = new PatientDTO
                                {
                                    statusDTO = new StatusDTO
                                    {
                                        Date = reader.GetDateTime(0),
                                        INR = reader.GetDecimal(1)
                                    }
                                };
                                patientStatus.Add(patientDTO);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (exception is InvalidCastException || exception is Microsoft.Data.SqlClient.SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
            return patientStatus;
        }

        public PatientDTO GetPatientAdditionalInfo(string idP)
        {
            PatientDTO patientDTO = new PatientDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_Doctor_GetPatientInfo", connectionString.sqlConnection);
                    cmd.Parameters.AddWithValue("@idP", idP);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                patientDTO = new PatientDTO
                                {
                                    Firstname = reader.GetString(0),
                                    Lastname = reader.GetString(1),
                                    Phonenumber = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    DateOfBirth = reader.GetDateTime(4).ToString("dd-MM-yyyy")
                                };
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (exception is InvalidCastException || exception is Microsoft.Data.SqlClient.SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
            return patientDTO;
        }
    }
}
