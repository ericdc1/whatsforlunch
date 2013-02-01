using System;
using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IVoteRepository
    {
        Vote GetItem(int id);
        Vote GetItem(int userID, DateTime date);
        IList<Vote> GetItemsByUser(int userID);
        IList<Vote> GetItemsByRestaurant(int restaurantID);
        IList<Vote> GetItemsByRestaurant(int restaurantID, DateTime date);
        IList<Vote> GetItemsByMonthAndYear(int month, int year);
        Vote SaveOrUpdate(Vote entity);
        void Delete(int id);
    }
}
