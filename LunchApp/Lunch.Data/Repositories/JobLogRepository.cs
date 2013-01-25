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
                return _connection.GetList<JobLog>(new {});
            }
        }

        public JobLog Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<JobLog>(id);
            }
        }

        public JobLog SaveOrUpdate(JobLog entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.JobLogId > 0)
                {
                    entity.JobLogId = _connection.Update(entity);
                }
                else
                {
                    var insert = _connection.Insert(entity);
                        entity.JobLogId = (int)insert;
                }
                return entity;
            }
        }

        public JobLog Delete(JobLog entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Delete(entity);
            }
            return entity;
        }
    }
}