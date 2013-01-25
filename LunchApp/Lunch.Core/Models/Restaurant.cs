using System;
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

    public class Restaurant : Template.Restaurant
    {
        #region persisted

        [Key]
        public override int Id { get; set; }
        public override string RestaurantName { get; set; }
        public override int? PreferredDayOfWeek { get; set; }
        public override int? RestaurantTypeID { get; set; }

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