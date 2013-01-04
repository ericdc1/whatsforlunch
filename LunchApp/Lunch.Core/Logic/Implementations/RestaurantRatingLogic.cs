using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantRatingLogic : IRestaurantRatingLogic 
    {
          private readonly IRestaurantRatingRepository _restaurantRatingRepository;

        public RestaurantRatingLogic(IRestaurantRatingRepository restaurantRatingRepository)
        {
            _restaurantRatingRepository = restaurantRatingRepository;
        }

        public IEnumerable<RestaurantRating> GetAllByUser(int UserID)
        {
            return _restaurantRatingRepository.GetAllByUser(UserID);
        }

        public RestaurantRating SaveOrUpdate(RestaurantRating entity)
        {
            return _restaurantRatingRepository.SaveOrUpdate(entity);
        }
    }
}
