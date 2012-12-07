using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class JobLogLogic : IJobLogLogic
    {
        private readonly IJobLogRepository _jobLogRepository;

        public JobLogLogic(IJobLogRepository restaurantRepository)
        {
            _jobLogRepository = restaurantRepository;
        }

        public IQueryable<JobLog> GetAll()
        {
            return _jobLogRepository.GetAll();
        }

        public IQueryable<JobLog> GetAll(JobLogDependencies dependencies)
        {
            return _jobLogRepository.GetAll(dependencies);
        }

        public IQueryable<JobLog> Get(System.Linq.Expressions.Expression<Func<JobLog, bool>> predicate)
        {
            return _jobLogRepository.Get(predicate);
        }

        public IEnumerable<JobLog> SaveOrUpdateAll(params JobLog[] entities)
        {
           return _jobLogRepository.SaveOrUpdateAll(entities);
        }

        public JobLog SaveOrUpdate(JobLog entity)
        {
            return _jobLogRepository.SaveOrUpdate(entity);
        }

        public JobLog Delete(JobLog entity)
        {
            return _jobLogRepository.Delete(entity);
        }
    }
}
