using System;

namespace Lunch.Core.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }
        public DayOfWeek? PreferredDayOfWeek { get; set; }
        public DateTime LastVisitedDate { get; set; }
    }
}

//ToDo: Create Add/Edit/Delete restaurants