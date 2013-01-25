using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;
using Lunch.Core.RepositoryInterfaces;
using Dapper;
namespace Lunch.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private DbConnection _connection;
        public IEnumerable<Restaurant> GetList(object parameters)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<Restaurant>(parameters);
            }
        }

        public IEnumerable<RestaurantDetails> GetAllDetailed(int? categoryId)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var result =_connection.Query<RestaurantDetails>(
                        @"Select restaurant.Id, restaurant.RestaurantName , restaurant.PreferredDayOfWeek, restauranttype.Id as RestaurantTypeID, restauranttype.typename as TypeName
                        from Restaurant
                        LEFT OUTER JOIN RestaurantType
                        ON Restaurant.RestaurantTypeId=Restauranttype.Id");
                if (categoryId != null)
                    result = result.Where(f => f.RestaurantTypeId == categoryId);

                return result;
            }
        }

        public Restaurant Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<Restaurant>(id);
            }
        }


        public IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities)
        {
            return entities.Select(SaveOrUpdate).ToList();
        }

        public Restaurant SaveOrUpdate(Restaurant entity)
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
                    entity.Id = insert;
                }
                return entity;
            }
        }

        public Restaurant Delete(Restaurant entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Delete(entity);
            }
            return entity;
        }
    }
}
