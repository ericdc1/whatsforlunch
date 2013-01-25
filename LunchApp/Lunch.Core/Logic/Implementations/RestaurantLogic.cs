using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantRatingLogic _restaurantRatingLogic;
        private readonly IVoteLogic _voteLogic;

        public RestaurantLogic(IRestaurantRepository restaurantRepository, IRestaurantRatingLogic restaurantRatingLogic, IVoteLogic voteLogic)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantRatingLogic = restaurantRatingLogic;
            _voteLogic = voteLogic;
        }


        public IEnumerable<Restaurant> GetTop(int count = 5)
        {
            var restaurants = _restaurantRepository.GetList(new {}).ToList();
            var ratings = _restaurantRatingLogic.GetAll().ToList();
            
            // multiply each users ratings by their weight


            // for each restaurant find the average
            foreach (var restaurant in restaurants)
            {
                var restaurantRatings = ratings.Where(r => r.RestaurantId == restaurant.Id).ToList();
                restaurant.Rating = (double)restaurantRatings.Sum(r => r.Rating) / (double)restaurantRatings.Count();
            }

            return restaurants.Take(count);
        }

        public IEnumerable<Restaurant> GetList(object parameters)
        {
            return _restaurantRepository.GetList(parameters);
        }

        public IEnumerable<Restaurant> GenerateRestaurants()
        {
            return _restaurantRepository.GetList(new{}).OrderBy (x => Guid.NewGuid()).Take(4);
        }

        public IEnumerable<RestaurantDetails> GetAllDetailed(int? categoryId)
        {
            return _restaurantRepository.GetAllDetailed(categoryId);
        }

        public Restaurant Get(int id)
        {
            return _restaurantRepository.Get(id);
        }

        public IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities)
        {
            return _restaurantRepository.SaveOrUpdateAll(entities);
        }

        public Restaurant SaveOrUpdate(Restaurant entity)
        {
            return _restaurantRepository.SaveOrUpdate(entity);
        }

        public Restaurant Delete(Restaurant entity)
        {
            return _restaurantRepository.Delete(entity);
        }
    }
}
