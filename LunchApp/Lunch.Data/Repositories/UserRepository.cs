using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Dapper;

namespace Lunch.Data.Repositories
{
    public class UserRepository : IJobRepository
    {

        // private DbConnection _connection;
        private LunchDatabase _rainbowconnection;

        public IEnumerable<Job> GetAll()
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                return _rainbowconnection.Jobs.All();
            }
        }

        public Job Get(int id)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                return _rainbowconnection.Jobs.Get(id);
            }
        }

        public Job SaveOrUpdate(Job entity)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                if (entity.Id > 0)
                {
                    entity.Id = _rainbowconnection.Jobs.Update(entity.Id, entity);
                }
                else
                {
                    var insert = _rainbowconnection.Jobs.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public Job Delete(Job entity)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                _rainbowconnection.Jobs.Delete(entity.Id);
            }
            return entity;
        }
    }
}