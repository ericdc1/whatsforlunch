using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAll();
        Job Get(int id);
        Job SaveOrUpdate(Job entity);
        Job Delete(Job entity);
    }
}