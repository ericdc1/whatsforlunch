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

        // private DbConnection _connection;
        private LunchDatabase _rainbowconnection;

        public IEnumerable<JobLog> GetAll()
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                return _rainbowconnection.JobLogs.All();
            }
        }

        public JobLog Get(int id)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                return _rainbowconnection.JobLogs.Get(id);
            }
        }

        public JobLog SaveOrUpdate(JobLog entity)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                if (entity.Id > 0)
                {
                    entity.Id = _rainbowconnection.JobLogs.Update(entity.Id, entity);
                }
                else
                {
                    var insert = _rainbowconnection.JobLogs.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public JobLog Delete(JobLog entity)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                _rainbowconnection.JobLogs.Delete(entity.Id);
            }
            return entity;
        }
    }
}