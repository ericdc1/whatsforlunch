using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class JobLogic : IJobLogic
    {
        private readonly IJobRepository _jobRepository;

        public JobLogic(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public IEnumerable<Job> GetAll()
        {
            return _jobRepository.GetAll();
        }

        public Job Get(int id)
        {
            return _jobRepository.Get(id);
        }

        public Job SaveOrUpdate(Job entity)
        {
            return _jobRepository.SaveOrUpdate(entity);
        }

        public Job Delete(Job entity)
        {
            return _jobRepository.Delete(entity);
        }
    }
}
