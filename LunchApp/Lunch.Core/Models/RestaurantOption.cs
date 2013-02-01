namespace Lunch.Core.Models
{
    public class RestaurantOption : Database.RestaurantOption
    {
        #region AdditionalFields

        public Restaurant Restaurant { get; set; }

        #endregion
    }
}