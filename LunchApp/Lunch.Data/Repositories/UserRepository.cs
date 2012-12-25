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
    public class UserRepository 
    {

         private DbConnection _connection;
       
        public IEnumerable<User> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<User>(new {});
            }
        }

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
                    var insert = _connection.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public User Delete(User entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Delete(entity);
            }
            return entity;
        }
    }
}