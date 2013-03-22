using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class DBVersionLogic : IDBVersionLogic
    {
        private readonly IDBVersionRepository _dbVersionRepository;

        public DBVersionLogic(IDBVersionRepository dbVersionRepository)
        {
            _dbVersionRepository = dbVersionRepository;
        }

        public IEnumerable<DBVersion> GetItems()
        {
            return _dbVersionRepository.GetItems();
        }

        public DBVersion GetLastVersion()
        {
            return _dbVersionRepository.GetLastVersion();
        }

        public DBVersion Insert(DBVersion entity)
        {
            entity.ID = _dbVersionRepository.Insert(entity);
            return entity;
        }

        public void CheckDbAccess(string connectionstring)
        {
            _dbVersionRepository.CheckDbAccess(connectionstring);
        }

        public void GenerateInitialDatabase(string connectionstring)
        {
            _dbVersionRepository.GenerateInitialDatabase(connectionstring);
        }
    }
}
