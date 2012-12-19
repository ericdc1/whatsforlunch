using System.ComponentModel;

namespace Lunch.Core.Models.Views
{
    public class RestaurantDetails
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string RestaurantName { get; set; }
        [DisplayName("Preferred Day")]
        public string PreferredDayOfWeek { get; set; }
        [DisplayName("Genre")]
        public string TypeName { get; set; }
        [DisplayName("Genre ID")]
        public int RestaurantTypeId { get; set; }
       
    }
}