using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IJobLogLogic
    {
        IQueryable<JobLog> GetAll();
        IQueryable<JobLog> GetAll(JobLogDependencies dependencies);
        IQueryable<JobLog> Get(Expression<Func<JobLog, bool>> predicate);
        IEnumerable<JobLog> SaveOrUpdateAll(params JobLog[] entities);
        JobLog SaveOrUpdate(JobLog entity);
        JobLog Delete(JobLog entity);
    }
}
