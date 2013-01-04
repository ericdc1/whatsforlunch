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
    public class RestaurantRatingRepository : IRestaurantRatingRepository
    {

        private DbConnection _connection;

        public IEnumerable<RestaurantRating> GetAllByUser(int UserID)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return
                    _connection.Query<RestaurantRating>(
                        "select RestaurantDetails.ID, RestaurantName, TypeName, COALESCE((Select Rating from RestaurantRatings where UserID = @UserID and RestaurantID = RestaurantDetails.ID),5) as Rating from restaurantdetails",
                        new {UserID = UserID});
            }
        }


        public RestaurantRating SaveOrUpdate(RestaurantRating entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {

                if (entity.ID > 0)
                {
                    _connection.Update(entity);
                }
                else
                {
                    var insert = _connection.Insert(entity);
                    entity.ID = (int)insert;
                }
                return entity;
            }
        }
    }
}