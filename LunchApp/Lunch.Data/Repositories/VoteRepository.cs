using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private DbConnection _connection;

        public Vote GetItem(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<Vote>(id);
            }
        }

        public Vote GetItem(int userID, DateTime date)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query("SELECT * FROM Votes " +
                                         "WHERE UserID = @UserID " +
                                         "AND DATEPART(mm, VoteDate) = DATEPART(mm, @VoteDate) " +
                                         "AND DATEPART(dd, VoteDate) = DATEPART(dd, @VoteDate) " +
                                         "AND DATEPART(yyyy, VoteDate) = DATEPART(yyyy, @VoteDate)", 
                                         new Vote { UserID = userID, VoteDate = date }).FirstOrDefault();
            }
        }

        public IList<Vote> GetItemsByUser(int userID)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<Vote>(new {UserID = userID}).ToList();
            }
        }

        public IList<Vote> GetItemsByRestaurant(int restaurantID)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<Vote>(new { RestaurantID = restaurantID }).ToList();
            }
        }

        public IList<Vote> GetItemsByRestaurant(int restaurantID, DateTime date)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Vote>("SELECT * FROM Votes WHERE RestaurantID = @RestaurantID " +
                                         "AND DATEPART(mm, VoteDate) = DATEPART(mm, @VoteDate) " +
                                         "AND DATEPART(dd, VoteDate) = DATEPART(dd, @VoteDate) " +
                                         "AND DATEPART(yyyy, VoteDate) = DATEPART(yyyy, @VoteDate)", 
                                         new Vote {RestaurantID = restaurantID, VoteDate = date}).ToList();
            }
        }

        public IList<Vote> GetItemsByMonthAndYear(int month, int year)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Vote>("SELECT * FROM Votes " +
                                               "WHERE DATEPART(mm, VoteDate) = @Month " +
                                               "AND DATEPART(yyyy, VoteDate) = @Year", 
                                               new { Month = month, Year = year}).ToList();
            }
        }

        public Vote SaveOrUpdate(Vote entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.Id == 0)
                    entity.Id = _connection.Insert(entity);
                else
                    _connection.Update(entity);
            }
            return entity;
        }
    }
}
