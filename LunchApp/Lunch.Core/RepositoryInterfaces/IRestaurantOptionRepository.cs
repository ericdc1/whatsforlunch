using System;
using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IRestaurantOptionRepository
    {
        IEnumerable<RestaurantOption> GetAllByDate(DateTime dateTime);
        IEnumerable<RestaurantOption> GetAll();
        IEnumerable<RestaurantOption> GetRecent();
        RestaurantOption SaveOrUpdate(RestaurantOption entity);
    }
}