using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IDBVersionRepository
    {
        IEnumerable<DBVersion> GetItems();
        DBVersion GetLastVersion();
        int Insert(DBVersion entity);
    }
}
