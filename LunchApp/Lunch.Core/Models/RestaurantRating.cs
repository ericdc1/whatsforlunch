using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lunch.Core.Models
{
    public class RestaurantRating : Database.RestaurantRating
    {
        #region AdditionalFields

        [Editable(false)]
        public string RestaurantName { get; set; }
        public Restaurant Restaurant { get; set; }
        [Editable(false)]
        public double WeightedRating { get; set; }

        #endregion

        public sealed class RestaurantRatingEqualityComparer : IEqualityComparer<RestaurantRating>
        {
            public bool Equals(RestaurantRating x, RestaurantRating y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.UserId.Equals(y.UserId) && x.RestaurantId.Equals(y.RestaurantId);
            }

            public int GetHashCode(RestaurantRating obj)
            {
                unchecked
                {
                    return (obj.UserId.GetHashCode()*397) ^ (obj.RestaurantId.GetHashCode());
                }
            }
        }

        private static readonly IEqualityComparer<RestaurantRating> RestaurantRatingComparerInstance = new RestaurantRatingEqualityComparer();

        public static IEqualityComparer<RestaurantRating> RestaurantRatingComparer
        {
            get { return RestaurantRatingComparerInstance; }
        }
    }
}