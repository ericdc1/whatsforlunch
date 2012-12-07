using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class JobLogLogic : IJobLogLogic
    {
        private readonly IJobLogRepository _restaurantRepository;

        public JobLogLogic(IJobLogRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public IQueryable<JobLog> GetAll()
        {
            return _restaurantRepository.GetAll();
        }

        public IQueryable<JobLog> GetAll(JobLogDependencies dependencies)
        {
            return _restaurantRepository.GetAll(dependencies);
        }

        public IQueryable<JobLog> Get(System.Linq.Expressions.Expression<Func<JobLog, bool>> predicate)
        {
            return _restaurantRepository.Get(predicate);
        }

        public IEnumerable<JobLog> SaveOrUpdateAll(params JobLog[] entities)
        {
           return _restaurantRepository.SaveOrUpdateAll(entities);
        }

        public JobLog SaveOrUpdate(JobLog entity)
        {
            return _restaurantRepository.SaveOrUpdate(entity);
        }

        public JobLog Delete(JobLog entity)
        {
            return _restaurantRepository.Delete(entity);
        }
    }
}
