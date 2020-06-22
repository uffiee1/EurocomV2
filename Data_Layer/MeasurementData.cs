using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EurocomV2_Data;
using Microsoft.Data.SqlClient;

namespace Data_Layer
{
    public class MeasurementData
    {
        public static void InsertMeasurement(MeasurementDTO measurement)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                connectionString.sqlConnection.Open();
                SqlCommand insert = new SqlCommand("sp_PatientMeasurements_InsertValue", connectionString.sqlConnection);
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.AddWithValue("@Date", measurement.measurementDate);
                insert.Parameters.AddWithValue("@MeasurementSucceeded", measurement.measurementSucceeded);
                insert.Parameters.AddWithValue("@Measurement", measurement.measurementValue);
                insert.ExecuteNonQuery();
            }
        }
    }
}
