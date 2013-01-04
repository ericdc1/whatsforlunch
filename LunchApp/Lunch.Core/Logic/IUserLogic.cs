﻿using System;
using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;

namespace Lunch.Core.Logic
{
    public interface IUserLogic
    {
        IEnumerable<User> GetList(object parameters);
        User Get(int id);
        User Get(Guid guid);
        User Update(User entity);
        int Delete(int id);
    }
}
