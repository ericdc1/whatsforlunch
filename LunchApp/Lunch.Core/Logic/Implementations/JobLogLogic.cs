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

        public JobLogLogic(IJobLogRepository jobLogRepository)
        {
            _jobLogRepository = jobLogRepository;
        }

        public IEnumerable<JobLog> GetAll()
        {
            return _jobLogRepository.GetAll();
        }

        public JobLog Get(int id)
        {
            return _jobLogRepository.Get(id);
        }
        public JobLog SaveOrUpdate(JobLog entity)
        {
            entity.LogDTM = DateTime.UtcNow;
            return _jobLogRepository.SaveOrUpdate(entity);
        }

        public JobLog Delete(JobLog entity)
        {
            return _jobLogRepository.Delete(entity);
        }
    }
}
