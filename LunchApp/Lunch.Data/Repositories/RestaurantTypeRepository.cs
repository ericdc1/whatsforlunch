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
                return _connection.GetList<RestaurantType>(new {});
            }
        }

        public RestaurantType Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<RestaurantType>(id);
            }
        }

        public RestaurantType SaveOrUpdate(RestaurantType entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.Id > 0)
                {
                    entity.Id = _connection.Update(entity);
                }
                else
                {
                    var insert = _connection.Insert(entity);
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public RestaurantType Delete(RestaurantType entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
               _connection.Delete(entity);
            }
            return entity;
        }
    }
}