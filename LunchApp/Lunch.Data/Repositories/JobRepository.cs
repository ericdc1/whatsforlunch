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
    public class JobRepository : IJobRepository
    {
        public ISession Session
        {
            get { return NHibernateHttpModule.GetCurrentSession(); }
        }

        public IQueryable<Job> GetAll()
        {
            return Session.Query<Job>();
        }

        public IQueryable<Job> GetAll(JobDependencies dependencies)
        {
            //var results = Session.QueryOver<Job>();

            //if ((dependencies & JobDependencies.JobHistories) == JobDependencies.JobHistories)
            //    results = results.Fetch(x => x.JobHistories).Eager;
            //if ((dependencies & JobDependencies.JobType) == JobDependencies.JobType)
            //    results = results.Fetch(y => y.JobType).Eager;

            //return results.Future<Job>().AsQueryable();
            return Session.Query<Job>();
        }

        public IQueryable<Job> Get(Expression<Func<Job, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public Job Load(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> SaveOrUpdateAll(params Job[] entities)
        {
            foreach (var entity in entities)
            {
                Session.SaveOrUpdate(entity);
            }

            return entities;
        }

        public Job SaveOrUpdate(Job entity)
        {
            Session.SaveOrUpdate(entity);

            return entity;
        }

        public Job Delete(Job entity)
        {
            Session.Delete(entity);

            return entity;
        }
    }
}