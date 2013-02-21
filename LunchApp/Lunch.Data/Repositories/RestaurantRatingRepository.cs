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


        public IEnumerable<RestaurantRating> GetAllByUser(int userId)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var results =
                    _connection.Query<RestaurantRating, Restaurant, RestaurantRating>(
                        @"SELECT RR.Id, RR.UserId, R.Id AS RestaurantId, 
                            COALESCE(RR.Rating, 5) AS Rating, R.* 
                            FROM Restaurants R
                            left outer join RestaurantRatings RR on RR.RestaurantId  = R.Id AND RR.UserId = @UserID ORDER BY R.RestaurantName", (rr, r) =>
                            {
                                rr.Restaurant = r;
                                return rr;
                            },
                        new { UserId = userId });

                return results;
            }
        }

        public IEnumerable<RestaurantRating> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var results =
                    _connection.Query<RestaurantRating>(
                        @"SELECT RR.Id, U.Id AS UserId, R.Id AS RestaurantId, 
                            COALESCE((SELECT Rating FROM RestaurantRatings RR WHERE RR.UserId = U.Id and RR.RestaurantId = R.Id), 5) AS Rating 
                            FROM Restaurants R
                            CROSS JOIN Users U
                            FULL OUTER JOIN RestaurantRatings RR ON RR.UserId = U.Id
                            ORDER BY R.Id, U.Id");

                return results;
            }
        }

        public RestaurantRating Insert(RestaurantRating entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var insert = _connection.Insert(entity);
                entity.Id = (int)insert;
                return entity;
            }
        }

        public RestaurantRating Delete(RestaurantRating entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Execute("Delete from RestaurantRatings where UserID = @UserID and RestaurantID = @RestaurantID", entity);
            }
            return entity;
        }
    }
}