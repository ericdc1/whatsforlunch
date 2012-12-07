using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
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

        public IQueryable<Restaurant> GetAll()
        {
            return _restaurantRepository.GetAll();
        }

        public IQueryable<Restaurant> GetAll(RestaurantDependencies dependencies)
        {
            return _restaurantRepository.GetAll(dependencies);
        }

        public IQueryable<Restaurant> Get(System.Linq.Expressions.Expression<Func<Restaurant, bool>> predicate)
        {
            return _restaurantRepository.Get(predicate);
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
