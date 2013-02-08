using System;
using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IVetoLogic
    {
        IEnumerable<Veto> GetAll();
        IEnumerable<Veto> GetAllActive();
        IEnumerable<Veto> GetAllActiveForUser(int userId);
        Veto Get(int id);
        Veto Get(DateTime usedDate);
        Veto SaveOrUpdate(Veto entity);
        Veto Delete(Veto entity); 
    }
}