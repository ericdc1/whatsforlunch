using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class JobLogic : IJobLogic
    {
        private readonly IJobRepository _restaurantRepository;

        public JobLogic(IJobRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public IQueryable<Job> GetAll()
        {
            return _restaurantRepository.GetAll();
        }

        public IQueryable<Job> GetAll(JobDependencies dependencies)
        {
            return _restaurantRepository.GetAll(dependencies);
        }

        public IQueryable<Job> Get(System.Linq.Expressions.Expression<Func<Job, bool>> predicate)
        {
            return _restaurantRepository.Get(predicate);
        }

        public IEnumerable<Job> SaveOrUpdateAll(params Job[] entities)
        {
           return _restaurantRepository.SaveOrUpdateAll(entities);
        }

        public Job SaveOrUpdate(Job entity)
        {
            return _restaurantRepository.SaveOrUpdate(entity);
        }

        public Job Delete(Job entity)
        {
            return _restaurantRepository.Delete(entity);
        }
    }
}
