using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Dapper;

namespace Lunch.Data.Repositories
{
    public class DBVersionRepository : IDBVersionRepository
    {
        private DbConnection _connection;

        public IEnumerable<DBVersion> GetItems()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<DBVersion>();
            }
        }

        public DBVersion GetLastVersion()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                const string query = "SELECT TOP(1) * FROM DBVersions ORDER BY ID DESC";
                return _connection.Query<DBVersion>(query).FirstOrDefault();
            }
        }

        public int Insert(DBVersion entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Insert(entity);
            }
        }

        public void CheckDbAccess(string connectionstring)
        {
            using (_connection = Utilities.GetOpenConnection(connectionstring))
            {
                _connection.Execute("CREATE TABLE testdb (Id int)");
                _connection.Execute("DROP TABLE testdb");
            }
        }


        public void GenerateInitialDatabase(string connectionstring)
        {
            //if database has versioninfo table ABORT
            using (_connection = Utilities.GetOpenConnection(connectionstring))
            {
                if (_connection.Query<string>("SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'dbversions'").Any())
                    throw new Exception("What are you doing? The database already exists!");
            }
            //Regular expression that finds multiline block comments.
            Regex findComments = new Regex(@"\/\*.*?\*\/", RegexOptions.Singleline | RegexOptions.Compiled);

            using (_connection = Utilities.GetOpenConnection(connectionstring))
            {
                var statements = Lunch.Data.Install.SQLresources._0001_InitialLoad;
                if (string.IsNullOrEmpty(statements))
                {
                    throw new Exception("The sql statement to execute is empty.");
                }

                string sqlBatch = string.Empty;
                foreach (string line in statements.Split(new string[2] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.ToUpperInvariant().Trim() == "GO")
                    {
                        _connection.Execute(sqlBatch);
                        sqlBatch = string.Empty;
                    }
                    else
                    {
                        sqlBatch += line + "\n";
                    }
                }

            }
        }
    }
}
