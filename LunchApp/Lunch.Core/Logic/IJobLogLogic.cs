using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IJobLogLogic
    {
        IEnumerable<JobLog> GetAll();
        JobLog Get(int id);
        JobLog SaveOrUpdate(JobLog entity);
        JobLog Delete(JobLog entity);
    }
}
