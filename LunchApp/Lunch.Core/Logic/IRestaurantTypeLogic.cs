using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IRestaurantTypeLogic
    {
        IEnumerable<RestaurantType> GetAll();
        RestaurantType Get(int id);
        RestaurantType SaveOrUpdate(RestaurantType entity);
        RestaurantType Delete(RestaurantType entity);
    }
}
