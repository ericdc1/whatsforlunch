using System;
using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;

namespace Lunch.Core.Logic
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User Get(Guid guid);
        IEnumerable<User> SaveOrUpdateAll(params User[] entities);
        User SaveOrUpdate(User entity);
        User Delete(User entity);
    }
}
