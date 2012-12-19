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
    public class JobRepository : IJobRepository
    {

        private DbConnection _connection;
     
        public IEnumerable<Job> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.Jobs.All();
            }
        }

        public Job Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.Jobs.Get(id);
            }
        }

        public Job SaveOrUpdate(Job entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                if (entity.Id > 0)
                {
                    entity.Id = db.Jobs.Update(entity.Id, entity);
                }
                else
                {
                    var insert = db.Jobs.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public Job Delete(Job entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                db.Jobs.Delete(entity.Id);
            }
            return entity;
        }
    }
}