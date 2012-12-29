using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.ViewModels
{
    public class ImportRestaurantsViewModel
    {
        private List<RestaurantViewModel> _restaurants = new List<RestaurantViewModel>();

        public int TotalResults { get; set; }
        public int Page { get; set; }
        public int FirstHit { get; set; }
        public int LastHit { get; set; }
        public List<RestaurantViewModel> Restaurants
        {
            get { return _restaurants; }
            set { _restaurants = value; }
        } 
    }

    public class RestaurantViewModel
    {
        public string  Name { get; set; }
        public bool Selected { get; set; }
        public DayOfWeek? SpecialDay { get; set; }
    }
}