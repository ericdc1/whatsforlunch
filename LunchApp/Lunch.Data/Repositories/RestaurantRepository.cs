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

        public IQueryable<Restaurant> GetAll(RestaurantDependencies dependencies)
        {
            //var results = Session.QueryOver<Restaurant>();

            //if ((dependencies & RestaurantDependencies.RestaurantHistories) == RestaurantDependencies.RestaurantHistories)
            //    results = results.Fetch(x => x.RestaurantHistories).Eager;
            //if ((dependencies & RestaurantDependencies.RestaurantType) == RestaurantDependencies.RestaurantType)
            //    results = results.Fetch(y => y.RestaurantType).Eager;

            //return results.Future<Restaurant>().AsQueryable();
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

        public Restaurant Delete(Restaurant entity)
        {
            Session.Delete(entity);

            return entity;
        }
    }
}