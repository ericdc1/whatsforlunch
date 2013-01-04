using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lunch.Website.ViewModels
{
    public class Restaurant
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Name")]
        public string RestaurantName { get;set; }

        [DisplayName("Preferred Day")]
        [Editable(true)]
        public int? PreferredDayOfWeek { get; set; }
        [DisplayName("Genre")]
        public int? RestaurantTypeID { get; set; }
    }
}