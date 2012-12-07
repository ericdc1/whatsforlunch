using System;
using System.Collections.Generic;

namespace Lunch.Core.Models
{
    public class Restaurant
    {
        public virtual int ID { get; protected set; }
        public virtual string RestaurantName { get; set; }
        public virtual DayOfWeek? PreferredDayOfWeek { get; set; }
        public virtual int RestaurantTypeID { get; set; }
        public virtual RestaurantType RestaurantType { get; set; }
        public virtual IList<RestaurantHistory> RestaurantHistories { get; set; }
    }
}