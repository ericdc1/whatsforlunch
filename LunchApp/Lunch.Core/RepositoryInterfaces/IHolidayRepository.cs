using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.RepositoryInterfaces
{
    public interface IHolidayRepository
    {
        IEnumerable<Holiday> GetList(object parameters);
        Holiday Insert(Holiday entity);
        Holiday Delete(Holiday entity);
    }
}