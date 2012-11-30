using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using NHibernate;
using NHibernate.Linq;

namespace Lunch.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public ISession Session
        {
            get { return NHibernateHttpModule.GetCurrentSession(); }
        }

        public IQueryable<Restaurant> GetAll()
        {
            return Session.Query<Restaurant>();
        }

        public IQueryable<Restaurant> Get(Expression<Func<Restaurant, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities)
        {
            foreach (var entity in entities)
            {
                Session.SaveOrUpdate(entity);
            }

            return entities;
        }

        public Restaurant SaveOrUpdate(Restaurant entity)
        {
            Session.SaveOrUpdate(entity);

            return entity;
        }
    }
}