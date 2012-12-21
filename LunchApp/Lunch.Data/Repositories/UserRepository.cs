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

        public IEnumerable<User> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.Users.All();
            }
        }

        public User Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.Users.Get(id);
            }
        }

        public User Get(Guid guid)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<User>("SELECT * FROM [User] WHERE GUID = @guid", new {guid = guid.ToString()}).FirstOrDefault();
            }
        }

        public IEnumerable<User> SaveOrUpdateAll(params User[] entities)
        {
            throw new NotImplementedException();
        }

        public User SaveOrUpdate(User entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                if (entity.Id > 0)
                {
                    entity.Id = db.Users.Update(entity.Id, entity);
                }
                else
                {
                    var insert = db.Users.Insert(entity);
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
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                db.Users.Delete(entity.Id);
            }
            return entity;
        }
    }
}