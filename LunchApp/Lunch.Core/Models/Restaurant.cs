﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public int? PreferredDayOfWeek { get; set; }
        public int? RestaurantTypeID { get; set; }

        #endregion

        #region relationships

        public virtual RestaurantType RestaurantType { get; set; }
        public virtual IList<RestaurantHistory> RestaurantHistories { get; set; }

        #endregion

        #region additional

        [Editable(false)]
        public double Rating { get; set; }

        #endregion
    }
}