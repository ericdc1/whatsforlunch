using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("RestaurantRatings")]
    public class RestaurantRating
    {
        #region DatabaseFields

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int Rating { get; set; }

        #endregion

        #region AdditionalFields

        [Editable(false)]
        public string RestaurantName { get; set; }

        #endregion
    }
}