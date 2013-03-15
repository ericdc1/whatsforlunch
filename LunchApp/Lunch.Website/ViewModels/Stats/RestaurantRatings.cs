using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Website.ViewModels.Stats
{
    public class RestaurantRatings
    {
        public List<Restaurant> TopTen { get; set; } 
        public List<Restaurant> TopTenWeighted { get; set; }
        public List<RestaurantOption> RestaurantOptions { get; set; }
    }
}