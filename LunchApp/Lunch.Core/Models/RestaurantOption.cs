using System.ComponentModel.DataAnnotations;

namespace Lunch.Core.Models
{
    public class RestaurantOption : Database.RestaurantOption
    {
        #region AdditionalFields

        public Restaurant Restaurant { get; set; }

        [Editable(false)]
        public int Votes { get; set; }

        #endregion
    }
}