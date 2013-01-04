using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("RestaurantRatings")]
    public class RestaurantRating
    {


        #region DatabaseFields
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RestaurantID { get; set; }
        public int Rating { get; set; }
        #endregion


        #region AdditionalFields
        [Editable(false)]
        public string RestaurantName { get; set; }
        #endregion



    }
}