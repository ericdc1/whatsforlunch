using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private DbConnection _connection;
        public IEnumerable<Restaurant> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.Restaurants.All();
            }
        }

        public IEnumerable<RestaurantDetails> GetAllDetailed(int? categoryId)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                var result =db.Query<RestaurantDetails>(
                        @"Select restaurant.Id, restaurant.RestaurantName , restaurant.PreferredDayOfWeek, restauranttype.Id as RestaurantTypeID, restauranttype.typename as TypeName
                        from Restaurant
                        INNER JOIN RestaurantType
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
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                return db.Restaurants.Get(id);
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
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);

                if (entity.Id > 0)
                {
                    db.Restaurants.Update(entity.Id, entity);
                }
                else
                {
                    var insert = db.Restaurants.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public Restaurant Delete(Restaurant entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var db = LunchDatabase.Init(_connection, commandTimeout: 2);
                db.Restaurants.Delete(entity.Id);
            }
            return entity;
        }
    }
}
