using System;
using System.Collections.Generic;

namespace Lunch.Core.Models
{
    /// <summary>
    /// Defines business model entites which are connected/dependent on Restaurant model.
    /// </summary>
    [Flags]
    public enum RestaurantTypeDependencies
    {
        Restaurants = 1
    }

    public class RestaurantType : Database.RestaurantType
    {

        public virtual IList<Restaurant> Restaurants { get; set; }
    }
}
