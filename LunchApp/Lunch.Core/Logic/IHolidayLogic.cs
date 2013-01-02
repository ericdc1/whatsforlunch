using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IHolidayLogic
    {
        IEnumerable<Holiday> GetAll();
        Holiday Insert(Holiday entity);
        Holiday Delete(Holiday entity);
    }
}
