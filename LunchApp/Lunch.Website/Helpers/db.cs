using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lunch.Website.Helpers
{
    public class Db
    {
        private static readonly ConnectionStringSettings Connection = ConfigurationManager.ConnectionStrings["AzureSQL"];
        private static readonly string ConnectionString = Connection.ConnectionString;
   
        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

    }
}