using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IJobLogic
    {
        IQueryable<Job> GetAll();
        IQueryable<Job> GetAll(JobDependencies dependencies);
        IQueryable<Job> Get(Expression<Func<Job, bool>> predicate);
        Job Load(int id);
        IEnumerable<Job> SaveOrUpdateAll(params Job[] entities);
        Job SaveOrUpdate(Job entity);
        Job Delete(Job entity);
    }
}
