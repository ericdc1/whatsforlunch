using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;

namespace Lunch.Core.Logic
{
    public interface IRestaurantLogic
    {
        IEnumerable<Restaurant> GetList(object parameters);
        IEnumerable<Restaurant> GenerateRestaurants();
        IEnumerable<RestaurantDetails> GetAllDetailed(int? categoryId);
        Restaurant Get(int id);
        IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities);
        Restaurant SaveOrUpdate(Restaurant entity);
        Restaurant Delete(Restaurant entity);
    }
}
