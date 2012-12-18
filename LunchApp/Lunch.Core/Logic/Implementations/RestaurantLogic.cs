using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantLogic(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurantRepository.GetAll();
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
