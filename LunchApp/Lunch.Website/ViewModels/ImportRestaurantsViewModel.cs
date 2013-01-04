using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.ViewModels
{
    public class ImportRestaurantsViewModel
    {
        private IList<RestaurantViewModel> _restaurants = new List<RestaurantViewModel>();

        public int TotalResults { get; set; }
        public int Page { get; set; }
        public int FirstHit { get; set; }
        public int LastHit { get; set; }
        public IList<RestaurantViewModel> Restaurants
        {
            get { return _restaurants; }
            set { _restaurants = value; }
        } 
    }

    public class RestaurantViewModel
    {
        public string RestaurantName { get; set; }
        public bool Selected { get; set; }
        public DayOfWeek? PreferredDayOfWeek { get; set; }
        public int? RestaurantTypeID { get; set; }
        public bool AlreadyImported { get; set; }

        public Core.Models.Restaurant ToDomainModel()
        {
            var entity = new Core.Models.Restaurant
                             { RestaurantName = RestaurantName, 
                               RestaurantTypeID = RestaurantTypeID,
                               PreferredDayOfWeek = (int?)PreferredDayOfWeek
                             };
            return entity;
        }
    }
}