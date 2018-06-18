using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Microsoft.Crm.Sdk.Samples
{

    public class SQLConnectionManager
    {
        public SqlConnection GetDatabaseConnection()
        {
            SqlConnection connection = null;
            string conn = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            connection = new SqlConnection(conn);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("Error connecting to database" + ex.Message);
            }

            return connection;
        }
    }
}
