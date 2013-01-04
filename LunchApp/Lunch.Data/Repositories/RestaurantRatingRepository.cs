using System;
using System.Collections.Generic;
using System.Data.Common;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Dapper;

namespace Lunch.Data.Repositories
{
    public class RestaurantRatingRepository : IRestaurantRatingRepository
    {
        private DbConnection _connection;


        public IEnumerable<RestaurantRating> GetAllByUser(int userID)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                // TODO: All kinds of broken

                var temp = 
                    _connection.Query<RestaurantRating>(
                        @"Select RestaurantRatings.Id AS Id, UserId, RestaurantId, 
                        COALESCE((Select Rating from RestaurantRatings where UserID = @UserID and RestaurantID = Restaurant.ID), 5) as Rating
                        from RestaurantRatings
                        INNER JOIN Restaurant
                        ON Restaurant.Id = RestaurantRatings.RestaurantId",
                        new {UserID = userID});

                return temp;
            }
        }

        public RestaurantRating SaveOrUpdate(RestaurantRating entity)
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
                    entity.Id = (int)insert;
                }
                return entity;
            }
        }
    }
}