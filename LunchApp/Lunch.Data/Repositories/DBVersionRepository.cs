using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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
    }
}
