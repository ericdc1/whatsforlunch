using System;

namespace Lunch.Core.Models
{
    public class Restaurant
    {
        public virtual int ID { get; protected set; }
        public virtual string RestaurantName { get; set; }
        public virtual DayOfWeek? PreferredDayOfWeek { get; set; }
        public virtual DateTime LastVisitedDate { get; set; }
    }
}

//ToDo: Create Add/Edit/Delete restaurants