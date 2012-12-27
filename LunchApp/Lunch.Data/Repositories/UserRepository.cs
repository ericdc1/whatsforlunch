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
    public class UserRepository : IUserRepository
    {

         private DbConnection _connection;

         public IEnumerable<User> GetList(object where)
         {
             using (_connection = Utilities.GetProfiledOpenConnection())
             {
                 return _connection.GetList<User>(where);
             }
         }

        public User Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<User>(id);
            }
        }

        public User Get(Guid guid)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<User>(new {GUID = guid}).FirstOrDefault();
            }
        }

        public User SaveOrUpdate(User entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.Id > 0)
                {
                    _connection.Update(entity);
                }
                else
                {
                    entity.Id = _connection.Insert(entity);
                }
                return entity;
            }
        }

        public int Delete(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Delete<User>(id);
            }
        }
    }
}