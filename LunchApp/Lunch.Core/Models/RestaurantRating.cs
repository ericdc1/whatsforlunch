using System.ComponentModel.DataAnnotations;

namespace Lunch.Core.Models
{
    public class RestaurantRating : Database.RestaurantRating
    {
        #region AdditionalFields

        [Editable(false)]
        public string RestaurantName { get; set; }

        #endregion
    }
}