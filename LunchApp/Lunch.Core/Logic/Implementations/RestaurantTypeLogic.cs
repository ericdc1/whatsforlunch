using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantTypeLogic : IRestaurantTypeLogic
    {
        private readonly IRestaurantTypeRepository _restaurantRepository;

        public RestaurantTypeLogic(IRestaurantTypeRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }


        public IQueryable<RestaurantType> GetAll()
        {
            return _restaurantRepository.GetAll();
        }

        public IQueryable<RestaurantType> GetAll(RestaurantTypeDependencies dependencies)
        {
            return _restaurantRepository.GetAll(dependencies);
        }

        public IQueryable<RestaurantType> Get(Expression<Func<RestaurantType, bool>> predicate)
        {
            return _restaurantRepository.Get(predicate);
        }

        public RestaurantType Load(int id)
        {
            return _restaurantRepository.Load(id);
        }

        public IEnumerable<RestaurantType> SaveOrUpdateAll(params RestaurantType[] entities)
        {
            return _restaurantRepository.SaveOrUpdateAll(entities);
        }

        public RestaurantType SaveOrUpdate(RestaurantType entity)
        {
            return _restaurantRepository.SaveOrUpdate(entity);
        }

        public RestaurantType Delete(RestaurantType entity)
        {
            return _restaurantRepository.Delete(entity);
        }
    }
}
