using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using EurocomV2_Data;
using Microsoft.Data.SqlClient;

namespace Data_Layer
{
    public class BoundaryData
    {
        public static InrDTO GetBoundaryData(string userID)
        {
            InrDTO inrDto = new InrDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                connectionString.sqlConnection.Open();
                SqlCommand check = new SqlCommand("sp_BoundaryValues_GetById", connectionString.sqlConnection);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@Id", userID);
                var reader = check.ExecuteReader();
                while (reader.Read())
                {
                    inrDto.id = userID;
                    inrDto.upperBoundary = reader.GetDecimal(0);
                    inrDto.targetValue = reader.GetDecimal(1);
                    inrDto.lowerBoundary = reader.GetDecimal(2);
                }
                connectionString.Dispose();
            }

            return inrDto;
        }

        public static bool CheckIfBoundaryDataExists(string userID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                int timesRead = 0;
                connectionString.sqlConnection.Open();
                SqlCommand check = new SqlCommand("sp_BoundaryValues_GetById", connectionString.sqlConnection);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@Id", userID);
                var reader = check.ExecuteReader();
                while (reader.Read())
                {
                    timesRead++;
                    if (reader.GetValue(0) == DBNull.Value || reader.GetValue(1) == DBNull.Value ||
                        reader.GetValue(2) == DBNull.Value)
                    {
                        connectionString.Dispose();
                        return false;
                    }
                }

                if (timesRead == 0)
                {
                    connectionString.Dispose();
                    return false;
                }
                connectionString.Dispose();
                return true;
            }
        }

        public static InrDTO GenerateBoundaryData(ClientDTO client)
        {
            Random rnd = new Random();
            InrDTO inrDto = new InrDTO()
            {
                client = client,
                id = new Guid().ToString(),
                lowerBoundary = Math.Round((decimal) rnd.NextDouble(0.7, 1.0), 2),
                targetValue = Math.Round((decimal) rnd.NextDouble(1.0, 1.5), 2),
                upperBoundary = Math.Round((decimal) rnd.NextDouble(1.0, 2.0), 2)
            };
            return inrDto;
        }

        public static void InsertBoundaryValues(string userID, decimal lowerBoundary, decimal upperBoundary, decimal targetValue)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                connectionString.sqlConnection.Open();
                SqlCommand insert = new SqlCommand("sp_BoundaryValues_InsertValue", connectionString.sqlConnection);
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.AddWithValue("@Id", userID);
                insert.Parameters.AddWithValue("@UpperBoundary", upperBoundary);
                insert.Parameters.AddWithValue("@LowerBoundary", lowerBoundary);
                insert.Parameters.AddWithValue("@TargetValue", targetValue);
                insert.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }
    }
}
