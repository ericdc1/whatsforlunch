using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Dapper;

namespace Lunch.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbConnection _connection;

        private readonly IVetoRepository _vetoRepository;

        public UserRepository(IVetoRepository vetoRepository)
        {
            _vetoRepository = vetoRepository;
        }


        public IEnumerable<User> GetList(object where)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                return _connection.GetList<User>(where);
            }
        }

        public IEnumerable<User> GetListByVotedDate(DateTime dateTime, UserDependencies? dependencies)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var users = _connection.Query<User>("SELECT * FROM Users U " +
                                               "INNER JOIN Votes V ON V.UserId = U.Id " +
                                               "AND DATEPART(mm, V.VoteDate) = DATEPART(mm, @VoteDate) " +
                                               "AND DATEPART(dd, V.VoteDate) = DATEPART(dd, @VoteDate) " +
                                               "AND DATEPART(yyyy, V.VoteDate) = DATEPART(yyyy, @VoteDate)",
                                               new Vote { VoteDate = dateTime }).ToList();

                if ((dependencies & UserDependencies.Vetoes) == UserDependencies.Vetoes)
                {
                    List<Veto> vetoes = null;
                    var userIds = (from u in users let userId = u.Id select userId).ToList();
                    if (userIds.Count > 0) vetoes = _vetoRepository.GetAll(userIds).ToList();
                    if (vetoes != null)
                        foreach (var user in users)
                            user.Vetoes = vetoes.FindAll(v => v.UserId == user.Id);
                }

                return users;
            }
        }

        public User Get(int id, UserDependencies? dependencies)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var user = _connection.Get<User>(id);

                if ((dependencies & UserDependencies.Vetoes) == UserDependencies.Vetoes)
                {
                    List<Veto> vetoes = null;
                    if (user != null) vetoes = _vetoRepository.GetAll(user.Id).ToList();
                    if (vetoes != null)
                        user.Vetoes = vetoes.FindAll(v => v.UserId == user.Id);
                }

                return user;
            }
        }

        public User Get(Guid guid, UserDependencies? dependencies)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var user = _connection.GetList<User>(new {GUID = guid}).FirstOrDefault();

                if ((dependencies & UserDependencies.Vetoes) == UserDependencies.Vetoes)
                {
                    List<Veto> vetoes = null;
                    if (user != null) vetoes = _vetoRepository.GetAll(user.Id).ToList();
                    if (vetoes != null)
                        user.Vetoes = vetoes.FindAll(v => v.UserId == user.Id);
                }

                return user;
            }
        }

        public User Get(string email, UserDependencies? dependencies)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                var user = _connection.GetList<User>(new { Email = email }).FirstOrDefault();

                if ((dependencies & UserDependencies.Vetoes) == UserDependencies.Vetoes)
                {
                    List<Veto> vetoes = null;
                    if (user != null) vetoes = _vetoRepository.GetAll(user.Id).ToList();
                    if (vetoes != null)
                        user.Vetoes = vetoes.FindAll(v => v.UserId == user.Id);
                }

                return user;
            }
        }

        public User Update(User entity)
        {
            using (_connection = Utilities.GetProfiledOpenConnection())
            {
                _connection.Update(entity);
                
                return entity;
            }
        }

        public int Delete(int id)
        {
            // Do not use this!
            // Use Simple Membership
            throw new NotImplementedException();
        }
    }
}