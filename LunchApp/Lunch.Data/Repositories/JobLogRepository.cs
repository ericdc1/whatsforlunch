using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using NHibernate;
using NHibernate.Linq;

namespace Lunch.Data.Repositories
{
    public class JobLogRepository : IJobLogRepository
    {
        public ISession Session
        {
            get { return NHibernateHttpModule.GetCurrentSession(); }
        }

        public IQueryable<JobLog> GetAll()
        {
            return Session.Query<JobLog>();
        }

        public IQueryable<JobLog> GetAll(JobLogDependencies dependencies)
        {
            //var results = Session.QueryOver<JobLog>();

            //if ((dependencies & JobLogDependencies.JobLogHistories) == JobLogDependencies.JobLogHistories)
            //    results = results.Fetch(x => x.JobLogHistories).Eager;
            //if ((dependencies & JobLogDependencies.JobLogType) == JobLogDependencies.JobLogType)
            //    results = results.Fetch(y => y.JobLogType).Eager;

            //return results.Future<JobLog>().AsQueryable();
            return Session.Query<JobLog>();
        }

        public IQueryable<JobLog> Get(Expression<Func<JobLog, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public JobLog Load(int id)
        {
            return Session.Load<JobLog>(id);
        }

        public IEnumerable<JobLog> SaveOrUpdateAll(params JobLog[] entities)
        {
            foreach (var entity in entities)
            {
                Session.SaveOrUpdate(entity);
            }

            return entities;
        }

        public JobLog SaveOrUpdate(JobLog entity)
        {
            Session.SaveOrUpdate(entity);

            return entity;
        }

        public JobLog Delete(JobLog entity)
        {
            Session.Delete(entity);

            return entity;
        }
    }
}