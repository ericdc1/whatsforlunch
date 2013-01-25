using System;
using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IVoteLogic
    {
        Vote GetItem(int id);
        Vote GetItem(int userID, DateTime? date);
        IList<Vote> GetItemsByUser(int userID);
        IList<Vote> GetItemsByRestaurant(int restaurantID);
        IList<Vote> GetItemsByRestaurant(int restaurantID, DateTime? date);
        Vote SaveVote(Vote entity);
        Vote SaveVote(int restaurantID, int userID);
    }
}
