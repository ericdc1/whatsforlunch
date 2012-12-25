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
                return _connection.GetList<Job>(new{});
            }
        }

        public Job Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<Job>(id);
            }
        }

        public Job SaveOrUpdate(Job entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.Id > 0)
                {
                    entity.Id =_connection.Update(entity);
                }
                else
                {
                     entity.Id = _connection.Insert(entity);
                }
                return entity;
            }
        }

        public Job Delete(Job entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Delete(entity);
            }
            return entity;
        }
    }
}