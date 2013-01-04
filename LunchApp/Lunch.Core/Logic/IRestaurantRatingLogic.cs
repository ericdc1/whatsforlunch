using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IRestaurantRatingLogic
    {
        IEnumerable<RestaurantRating> GetAllByUser(int UserID);
        RestaurantRating SaveOrUpdate(RestaurantRating entity);
    }
}
