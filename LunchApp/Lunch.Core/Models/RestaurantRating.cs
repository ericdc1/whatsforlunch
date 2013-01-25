using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("RestaurantRatings")]
    public class RestaurantRating : Database.RestaurantRating
    {

        #region AdditionalFields

        [Editable(false)]
        public string RestaurantName { get; set; }

        #endregion
    }
}