using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core;
using NHibernate;
using NHibernate.Linq;

namespace Lunch.Data
{
    public class Repository<T> : IRepository<T>
    {
        public ISession Session
        {
            get { return NHibernateHttpModule.GetCurrentSession(); }
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IEnumerable<T> SaveOrUpdateAll(params T[] entities)
        {
            foreach (var entity in entities)
            {
                Session.SaveOrUpdate(entity);
            }

            return entities;
        }

        public T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);

            return entity;
        }
    }
}