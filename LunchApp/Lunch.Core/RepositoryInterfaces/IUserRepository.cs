using System;
using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetList(object parameters);
        IEnumerable<User> GetListByVotedDate(DateTime dateTime, UserDependencies? dependencies);
        User Get(int id, UserDependencies? dependencies);
        User Get(Guid guid, UserDependencies? dependencies);
        User Get(string email, UserDependencies? dependencies);
        User Update(User entity);
        int Delete(int id);
    }
}