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
                    _connection.Query<RestaurantRating>(
                        @"SELECT RR.Id, RR.UserId, R.Id AS RestaurantId, 
                            COALESCE((SELECT Rating FROM RestaurantRatings RR WHERE RR.UserID = @UserId and RR.RestaurantId = R.Id), 5) AS Rating 
                            FROM Restaurant R
                            CROSS JOIN RestaurantRatings RR
                            WHERE RR.UserID = @UserId",
                        new {UserId = userId});

                return results;
            }
        }

        public IEnumerable<RestaurantRating> GetAll()
        {
            using(_connection = Utilities.GetProfiledOpenConnection())
            {
                var results = 
                    _connection.Query<RestaurantRating>(
                        @"SELECT RR.Id, U.Id AS UserId, R.Id AS RestaurantId, 
                            COALESCE((SELECT Rating FROM RestaurantRatings RR WHERE RR.UserId = U.Id and RR.RestaurantId = R.Id), 5) AS Rating 
                            FROM Restaurant R
                            CROSS JOIN Users U
                            FULL OUTER JOIN RestaurantRatings RR ON RR.UserId = U.Id
                            ORDER BY R.Id, U.Id");

                return results;
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