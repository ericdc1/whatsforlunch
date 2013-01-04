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

        public RestaurantLogic(IRestaurantRepository restaurantRepository, IRestaurantRatingLogic restaurantRatingLogic)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantRatingLogic = restaurantRatingLogic;
        }


        public IEnumerable<Restaurant> GetTop(int count = 5)
        {
            var restaurants = _restaurantRepository.GetList(new {}).ToList();

            var allRatings = _restaurantRatingLogic.GetAll().ToList();

            foreach (var restaurant in restaurants)
            {
                var i = restaurant;
                var ratings = allRatings.Where(r => r.Id == i.Id).ToList();
                restaurant.Rating = ratings.Sum(r => r.Rating) / ratings.Count();
            }

            return restaurants;
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
