using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IRestaurantRatingRepository
    {
        IEnumerable<RestaurantRating> GetAllByUser(int userID);
        RestaurantRating SaveOrUpdate(RestaurantRating entity);
    }
}