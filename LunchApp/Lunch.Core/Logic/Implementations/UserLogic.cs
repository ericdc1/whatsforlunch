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

        public IEnumerable<User> GetListByVotedDate(DateTime? dateTime, UserDependencies? dependencies)
        {
            if (dateTime == null) dateTime = Core.Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow);

            return _userRepository.GetListByVotedDate(dateTime.Value, dependencies);
        }

        public User Get(int id, UserDependencies? dependencies)
        {
            return _userRepository.Get(id, dependencies);
        }

        public User Get(Guid guid, UserDependencies? dependencies)
        {
            return _userRepository.Get(guid, dependencies);
        }

        public User Get(string email, UserDependencies? dependencies)
        {
            return _userRepository.Get(email, dependencies);
        }

        public User Update(User entity)
        {
            var current = Get(entity.Id, null);

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