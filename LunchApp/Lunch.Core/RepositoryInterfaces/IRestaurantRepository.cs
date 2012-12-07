using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IRestaurantRepository
    {
        IQueryable<Restaurant> GetAll();
        IQueryable<Restaurant> Get(Expression<Func<Restaurant, bool>> predicate);
        IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities);
        Restaurant SaveOrUpdate(Restaurant entity);
        Restaurant Delete(Restaurant entity);
    }
}