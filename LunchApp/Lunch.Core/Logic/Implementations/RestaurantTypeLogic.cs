using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantTypeLogic : IRestaurantTypeLogic
    {
        private readonly IRestaurantTypeRepository _restaurantTypeRepository;

        public RestaurantTypeLogic(IRestaurantTypeRepository restaurantTypeRepository)
        {
            _restaurantTypeRepository = restaurantTypeRepository;
        }


        public IEnumerable<RestaurantType> GetAll()
        {
            return _restaurantTypeRepository.GetAll();
        }

        public RestaurantType Get(int id)
        {
            return _restaurantTypeRepository.Get(id);
        }

        public RestaurantType SaveOrUpdate(RestaurantType entity)
        {
            return _restaurantTypeRepository.SaveOrUpdate(entity);
        }

        public RestaurantType Delete(RestaurantType entity)
        {
            return _restaurantTypeRepository.Delete(entity);
        }
    }
}
