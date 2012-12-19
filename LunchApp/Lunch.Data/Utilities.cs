using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using StackExchange.Profiling;

namespace Lunch.Data
{
    public class Utilities
    {
        private static readonly ConnectionStringSettings Connection = ConfigurationManager.ConnectionStrings["AzureSQL"];
        private static readonly string ConnectionString = Connection.ConnectionString;
   
        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static DbConnection GetProfiledOpenConnection()
        {
            var cnn = GetOpenConnection();

            // wrap the connection with a profiling connection that tracks timings 
            return new StackExchange.Profiling.Data.ProfiledDbConnection(cnn, MiniProfiler.Current);
        }

        public static LunchDatabase GetProfiledOpenRainbowConnection()
        {
            var cnn = GetOpenConnection();

            // wrap the connection with a profiling connection that tracks timings and initialize a LunchDatabase
            return LunchDatabase.Init(GetProfiledOpenConnection(), commandTimeout: 2);
        }

    }
}
