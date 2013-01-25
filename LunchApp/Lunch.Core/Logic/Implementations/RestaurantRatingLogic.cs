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
        private readonly IUserLogic _userLogic;

        public RestaurantRatingLogic(IRestaurantRatingRepository restaurantRatingRepository, IUserLogic userLogic)
        {
            _restaurantRatingRepository = restaurantRatingRepository;
            _userLogic = userLogic;
        }

        public IEnumerable<RestaurantRating> GetAll()
        {
            var ratings = _restaurantRatingRepository.GetAll();

            return ratings;
        }

        public IEnumerable<RestaurantRating> GetAllByUser(int userID)
        {
            return _restaurantRatingRepository.GetAllByUser(userID);
        }

        public RestaurantRating SaveOrUpdate(RestaurantRating entity)
        {
            return _restaurantRatingRepository.SaveOrUpdate(entity);
        }
    }
}
