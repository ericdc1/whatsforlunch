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
    public class RestaurantTypeRepository : IRestaurantTypeRepository
    {

       private DbConnection _connection;

        public IEnumerable<RestaurantType> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.RestaturantTypes.All();
            }
        }

        public RestaurantType Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.RestaturantTypes.Get(id);
            }
        }

        public RestaurantType SaveOrUpdate(RestaurantType entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                if (entity.Id > 0)
                {
                    entity.Id = db.RestaturantTypes.Update(entity.Id, entity);
                }
                else
                {
                    var insert = db.RestaturantTypes.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public RestaurantType Delete(RestaurantType entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                db.RestaturantTypes.Delete(entity.Id);
            }
            return entity;
        }
    }
}