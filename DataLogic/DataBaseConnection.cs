using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EurocomV2.DataLogic
{
    public class DataBaseConnection
    {
        public SqlConnection connection = new SqlConnection(@"Server=mssql.fhict.local;Database=dbi406383_eurocom;User Id=dbi406383_eurocom;Password=Kastanje81;");
    }
}
