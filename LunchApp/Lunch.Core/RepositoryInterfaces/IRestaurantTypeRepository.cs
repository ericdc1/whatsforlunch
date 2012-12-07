using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IRestaurantTypeRepository
    {
        IQueryable<RestaurantType> GetAll();
        IQueryable<RestaurantType> GetAll(RestaurantTypeDependencies dependencies);
        IQueryable<RestaurantType> Get(Expression<Func<RestaurantType, bool>> predicate);
        IEnumerable<RestaurantType> SaveOrUpdateAll(params RestaurantType[] entities);
        RestaurantType SaveOrUpdate(RestaurantType entity);
        RestaurantType Delete(RestaurantType entity);
    }
}