using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IVetoRepository
    {
        IEnumerable<Veto> GetAll();
        IEnumerable<Veto> GetAllActive();
        IEnumerable<Veto> GetAllActiveForUser(int userId);
        IEnumerable<Veto> GetAll(List<int> userIds);
        IEnumerable<Veto> GetAll(int userId);
        Veto Get(int id);
        Veto Get(DateTime usedDate);
        Veto SaveOrUpdate(Veto entity);
        Veto Delete(Veto entity);
    }
}