using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IDBVersionLogic
    {
        IEnumerable<DBVersion> GetItems();
        DBVersion GetLastVersion();
        DBVersion Insert(DBVersion entity);
    }
}
