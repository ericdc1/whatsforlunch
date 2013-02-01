using System;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Data.Repositories
{
    public class RestaurantOptionRepository : IRestaurantOptionRepository
    {
        private DbConnection _connection;

        #region IRestaurantOptionRepository Members

        public IEnumerable<RestaurantOption> GetAllByDate(DateTime dateTime)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                IEnumerable<RestaurantOption> results =
                    _connection.Query<RestaurantOption, Restaurant, RestaurantOption>(@"SELECT * FROM RestaurantOptions RO
                                                                                        INNER JOIN Restaurants R ON R.Id = RO.RestaurantId
                                                                                        WHERE SelectedDate = @date",
                                                                                      (ro, r) =>
                                                                                          {
                                                                                              ro.Restaurant = r;
                                                                                              return ro;
                                                                                          }, new {date = dateTime.Date});

                return results;
            }
        }

        public IEnumerable<RestaurantOption> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                IEnumerable<RestaurantOption> results =
                    _connection.Query<RestaurantOption, Restaurant, RestaurantOption>(@"SELECT * FROM RestaurantOptions RO
                                                                                        INNER JOIN Restaurants R ON R.Id = RO.RestaurantId ",
                                                                                      (ro, r) =>
                                                                                          {
                                                                                              ro.Restaurant = r;
                                                                                              return ro;
                                                                                          });

                return results;
            }
        }

        public RestaurantOption SaveOrUpdate(RestaurantOption entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.Id > 0)
                {
                    _connection.Update(entity);
                }
                else
                {
                    int insert = _connection.Insert(entity);
                    entity.Id = insert;
                }
                return entity;
            }
        }

        #endregion
    }
}