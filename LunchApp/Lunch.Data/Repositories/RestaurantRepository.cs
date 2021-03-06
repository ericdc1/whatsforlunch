﻿using System;
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
                        @"Select Restaurants.Id, Restaurants.RestaurantName, Restaurants.PreferredDayOfWeek, RestaurantTypes.Id as RestaurantTypeId, RestaurantTypes.TypeName as TypeName
                        from Restaurants
                        LEFT OUTER JOIN RestaurantTypes
                        ON Restaurants.RestaurantTypeId = RestaurantTypes.Id");
                if (categoryId != null)
                    result = result.Where(f => f.RestaurantTypeId == categoryId);
                return result;
            }
        }

        public IEnumerable<Restaurant> GetAllByVote(DateTime dateTime)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var result = _connection.Query<Restaurant>(
                        @"SELECT R.*, (SELECT COUNT(*) AS Votes FROM Votes V WHERE V.RestaurantId = R.Id AND V.VoteDate > @start AND V.VoteDate < @end) Votes
                            FROM Restaurants R
                            ORDER BY Votes DESC
                            ", new {start = dateTime.Date.AddDays(-1),
                                    end = dateTime.Date.AddDays(1)});

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
