using System;
using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetList(object parameters)
        {
            return _userRepository.GetList(parameters);
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public User Get(Guid guid)
        {
            return _userRepository.Get(guid);
        }

        public User SaveOrUpdate(User entity)
        {
            return _userRepository.SaveOrUpdate(entity);
        }

        public User Delete(User entity)
        {
            return _userRepository.Delete(entity);
        }
    }
}