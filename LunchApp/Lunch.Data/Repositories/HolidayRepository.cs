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
    public class HolidayRepository : IHolidayRepository
    {

        private DbConnection _connection;

        public IEnumerable<Holiday> GetList(object parameters)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<Holiday>(parameters);
            }
        }

     
        public Holiday Insert(Holiday entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                entity.Id = _connection.Insert(entity);
                return entity;
            }
        }

        public Holiday Delete(Holiday entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Delete(entity);
            }
            return entity;
        }
    }
}