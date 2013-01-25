﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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

        public User Get(string email)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<User>(new { Email = email }).FirstOrDefault();
            }
        }

        public User Update(User entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Update(entity);
                
                return entity;
            }
        }

        public int Delete(int id)
        {
            // Do not use this!
            // Use Simple Membership
            throw new NotImplementedException();
        }
    }
}