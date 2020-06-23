using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EurocomV2_Data.DTO;

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

        public void RemovePatientLinkedToDoctor(string username, string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Doctor_RemovePatient", connectionString.sqlConnection);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
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
        }

        public List<PatientDTO> GetPatientStatus(string id)
        {
            List<PatientDTO> patientStatus = new List<PatientDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Doctor_GetMeasurements", connectionString.sqlConnection);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
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
                    if (exception is InvalidCastException || exception is SqlException)
                    {
                        Console.WriteLine("Error source: " + exception);
                        throw;
                    }
                }
            }
            return patientStatus;
        }

        public PatientDTO GetPatientAdditionalInfo(string id)
        {
            PatientDTO patientDTO = new PatientDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                try
                {
                    connectionString.sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Doctor_GetPatientInfo", connectionString.sqlConnection);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
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
                    if (exception is InvalidCastException || exception is SqlException)
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
