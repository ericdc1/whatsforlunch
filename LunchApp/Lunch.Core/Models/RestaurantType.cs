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

    public class RestaurantType
    {
        public virtual int Id { get; protected set; }
        public virtual string TypeName { get; set; }
        public virtual IList<Restaurant> Restaurants { get; set; }
    }
}
