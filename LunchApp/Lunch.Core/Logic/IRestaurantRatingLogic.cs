using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IRestaurantRatingLogic
    {
        IEnumerable<RestaurantRating> GetAll();
        IEnumerable<RestaurantRating> GetAllByUser(int userId);
        RestaurantRating Insert(RestaurantRating entity);
    }
}
