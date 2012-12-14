using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using NHibernate;
using NHibernate.Linq;

namespace Lunch.Data.Repositories
{
    public class RestaurantTypeRepository : IRestaurantTypeRepository
    {
        public ISession Session
        {
            get { return NHibernateHttpModule.GetCurrentSession(); }
        }

        public IQueryable<RestaurantType> GetAll()
        {
            return Session.Query<RestaurantType>();
        }

        public IQueryable<RestaurantType> GetAll(RestaurantTypeDependencies dependencies)
        {
            var results = Session.QueryOver<RestaurantType>();

            if ((dependencies & RestaurantTypeDependencies.Restaurants) == RestaurantTypeDependencies.Restaurants)
                results = results.Fetch(x => x.Restaurants).Eager;

            return results.Future<RestaurantType>().AsQueryable();
        }

        public IQueryable<RestaurantType> Get(Expression<Func<RestaurantType, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public RestaurantType Load(int id)
        {
            return Session.Load<RestaurantType>(id);
        }

        public IEnumerable<RestaurantType> SaveOrUpdateAll(params RestaurantType[] entities)
        {
            foreach (var entity in entities)
            {
                Session.SaveOrUpdate(entity);
            }

            return entities;
        }

        public RestaurantType SaveOrUpdate(RestaurantType entity)
        {
            Session.SaveOrUpdate(entity);

            return entity;
        }

        public RestaurantType Delete(RestaurantType entity)
        {
            Session.Delete(entity);

            return entity;
        }
    }
}