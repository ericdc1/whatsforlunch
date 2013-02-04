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

        public IEnumerable<User> GetListByVotedDate(DateTime? dateTime)
        {
            if (dateTime == null) dateTime = DateTime.Now;

            return _userRepository.GetListByVotedDate(dateTime.Value);
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public User Get(Guid guid)
        {
            return _userRepository.Get(guid);
        }

        public User Get(string email)
        {
            return _userRepository.Get(email);
        }

        public User Update(User entity)
        {
            var current = Get(entity.Id);

            if (entity.Guid == Guid.Empty || entity.Guid == null)
                entity.Guid = current.Guid;
            if (String.IsNullOrWhiteSpace(entity.Email))
                entity.Email = current.Email;

            return _userRepository.Update(entity);
        }

        public int Delete(int id)
        {
            // Do not use this!
            // Use Simple Membership
            throw new NotImplementedException();
        }
    }
}