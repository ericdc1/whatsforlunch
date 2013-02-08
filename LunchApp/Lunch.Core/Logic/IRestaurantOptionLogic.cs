using System;
using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IRestaurantOptionLogic
    {
        IEnumerable<RestaurantOption> GetAll();
        IEnumerable<RestaurantOption> GetAllByDate(DateTime? dateTime);
        IEnumerable<RestaurantOption> GetAndSaveOptions();
        RestaurantOption FinalizeOptions();
        RestaurantOption TodaysSelection();
        RestaurantOption SaveOrUpdate(RestaurantOption entity);
    }
}
