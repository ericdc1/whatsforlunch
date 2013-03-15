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

        public sealed class RestaurantEqualityComparer : IEqualityComparer<Restaurant>
        {
            public bool Equals(Restaurant x, Restaurant y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id;
            }

            public int GetHashCode(Restaurant obj)
            {
                unchecked
                {
                    return (obj.Id.GetHashCode()*397);
                }
            }
        }

        public static readonly IEqualityComparer<Restaurant> RestaurantComparerInstance = new RestaurantEqualityComparer();

        public static IEqualityComparer<Restaurant> RestaurantComparer
        {
            get { return RestaurantComparerInstance; }
        }
    }
}