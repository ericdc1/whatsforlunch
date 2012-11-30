using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.Models
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