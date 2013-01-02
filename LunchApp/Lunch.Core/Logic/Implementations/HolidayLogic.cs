using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class HolidayLogic : IHolidayLogic 
    {
          private readonly IHolidayRepository _holidayRepository;

        public HolidayLogic(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public IEnumerable<Holiday> GetAll()
        {
            return _holidayRepository.GetList(new {});
        }

        public Holiday Insert(Holiday entity)
        {
            entity.ExcludedDate = Convert.ToDateTime(entity.ExcludedDate.ToShortDateString());
            return _holidayRepository.Insert(entity);
        }

        public Holiday Delete(Holiday entity)
        {
            entity.ExcludedDate = Convert.ToDateTime(entity.ExcludedDate.ToShortDateString());
            var selecteddate = _holidayRepository.GetList(new {ExcludedDate = entity.ExcludedDate}).FirstOrDefault();
            return _holidayRepository.Delete(selecteddate);
        }
    }
}
