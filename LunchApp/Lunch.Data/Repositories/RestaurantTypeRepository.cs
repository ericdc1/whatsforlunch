using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Dapper;
namespace Lunch.Data.Repositories
{
    public class RestaurantTypeRepository : IRestaurantTypeRepository
    {

       // private DbConnection _connection;
        private LunchDatabase _rainbowconnection;

        public IEnumerable<RestaurantType> GetAll()
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                return _rainbowconnection.RestaturantTypes.All();
            }
        }

        public RestaurantType Get(int id)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                return _rainbowconnection.RestaturantTypes.Get(id);
            }
        }

        public RestaurantType SaveOrUpdate(RestaurantType entity)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                if (entity.Id > 0)
                {
                    entity.Id = _rainbowconnection.RestaturantTypes.Update(entity.Id, entity);
                }
                else
                {
                    var insert = _rainbowconnection.RestaturantTypes.Insert(entity);
                    if (insert != null)
                        entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public RestaurantType Delete(RestaurantType entity)
        {
            using (_rainbowconnection = Utilities.GetProfiledOpenRainbowConnection())
            {
                _rainbowconnection.RestaturantTypes.Delete(entity.Id);
            }
            return entity;
        }
    }
}