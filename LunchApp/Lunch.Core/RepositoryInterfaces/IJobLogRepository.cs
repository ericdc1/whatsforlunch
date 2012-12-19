using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IJobLogRepository
    {
        IEnumerable<JobLog> GetAll();
        JobLog Get(int id);
        JobLog SaveOrUpdate(JobLog entity);
        JobLog Delete(JobLog entity);
    }
}