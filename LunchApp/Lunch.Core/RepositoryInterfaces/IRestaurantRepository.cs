using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetList(object parameters);
        IEnumerable<RestaurantDetails> GetAllDetailed(int? categoryId);
        Restaurant Get(int id);
        IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities);
        Restaurant SaveOrUpdate(Restaurant entity);
        Restaurant Delete(Restaurant entity);
    }
}