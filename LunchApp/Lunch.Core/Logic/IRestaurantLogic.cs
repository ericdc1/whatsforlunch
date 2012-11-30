using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IRestaurantLogic
    {
        IQueryable<Restaurant> GetAll();
        IQueryable<Restaurant> Get(Expression<Func<Restaurant, bool>> predicate);
        IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities);
        Restaurant SaveOrUpdate(Restaurant entity);
        bool Delete(int ID);
    }
}
