using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

namespace EurocomV2_Data
{
    public class ConnectionString : IDisposable
    {
        public SqlConnection sqlConnection = new SqlConnection
        (
            //@"Server=mssql.fhict.local;Database=dbi406383_eurocom;User Id=dbi406383_eurocom;Password=Kastanje81;"
            @"Server=mssql.fhict.local;Database=dbi406383_eurocomv2;User Id=dbi406383_eurocomv2;Password=Handjeklap1234;"
        );

        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
