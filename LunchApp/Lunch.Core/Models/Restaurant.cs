using System;
using System.Collections.Generic;

namespace Lunch.Core.Models
{
    /// <summary>
    /// Defines business model entites which are connected/dependent on Restaurant model.
    /// </summary>
    [Flags]
    public enum RestaurantDependencies
    {
        RestaurantType = 1,
        RestaurantHistories = 2
    }

    public class Restaurant
    {
        #region persisted

        public virtual int Id { get; set; }
        public virtual string RestaurantName { get; set; }
        public virtual string PreferredDayOfWeek { get; set; }
        public virtual int RestaurantTypeID { get; set; }

        #endregion

        #region relationships

        public virtual RestaurantType RestaurantType { get; set; }
        public virtual IList<RestaurantHistory> RestaurantHistories { get; set; }

        #endregion
    }
}