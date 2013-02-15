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
    public class VetoRepository : IVetoRepository
    {

       private DbConnection _connection;


        public IEnumerable<Veto> GetAll()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<Veto>(new {});
            }
        }

        public IEnumerable<Veto> GetAllActive()
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Veto>(@"SELECT * FROM Vetoes WHERE Used = 0").ToList();
            }
        }

        public IEnumerable<Veto> GetAllActiveForUser(int userId)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Veto>(@"SELECT * FROM Vetoes WHERE Used = 0 AND UserId = @userId", new {userId}).ToList();
            }
        }

        public IEnumerable<Veto> GetAll(List<int> userIds)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Veto>(@"SELECT * FROM Vetoes WHERE Used = 0 AND UserId IN @userIds",new {userIds}).ToList();
            }
        }

        public IEnumerable<Veto> GetAll(int userId)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Veto>(@"SELECT * FROM Vetoes WHERE Used = 0 AND UserId = @userId", new { userId }).ToList();
            }
        }

        public Veto Get(DateTime usedAt)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Query<Veto>(@"SELECT * FROM Vetoes V WHERE 
DATEPART(mm, V.UsedAt) = DATEPART(mm, @usedAt) 
AND DATEPART(dd, V.UsedAt) = DATEPART(dd, @usedAt)
AND DATEPART(yyyy, V.UsedAt) = DATEPART(yyyy, @usedAt)", new { usedAt = usedAt }).FirstOrDefault();
            }
        }

        public Veto Get(int id)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.Get<Veto>(id);
            }
        }

        public Veto SaveOrUpdate(Veto entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                if (entity.Id > 0)
                {
                    entity.Id = _connection.Update(entity);
                }
                else
                {
                    var insert = _connection.Insert(entity);
                    entity.Id = (int)insert;
                }
                return entity;
            }
        }

        public Veto Delete(Veto entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Delete(entity);
            }
            return entity;
        }
    }
}