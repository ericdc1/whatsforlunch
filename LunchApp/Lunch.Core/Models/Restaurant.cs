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
        RestaurantType = 1
    }

    public class Restaurant : Database.Restaurant
    {
        #region relationships

        public virtual RestaurantType RestaurantType { get; set; }

        #endregion

        #region additional

        [Editable(false)]
        public double Rating { get; set; }

        [Editable(false)]
        public int Votes { get; set; }

        #endregion
    }
}