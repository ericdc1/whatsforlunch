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
    }
}