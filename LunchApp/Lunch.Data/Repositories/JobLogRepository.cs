using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Dapper;
namespace Lunch.Data.Repositories
{
    public class JobLogRepository : IJobLogRepository
    {

        private DbConnection _connection;
       
        public IEnumerable<JobLog> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.JobLogs.All();
            }
        }

        public JobLog Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.JobLogs.Get(id);
            }
        }

        public JobLog SaveOrUpdate(JobLog entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                if (entity.Id > 0)
                {
                    entity.Id = db.JobLogs.Update(entity.Id, entity);
                }
                else
                {
                    var insert = db.JobLogs.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public JobLog Delete(JobLog entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                 var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                db.JobLogs.Delete(entity.Id);
            }
            return entity;
        }
    }
}