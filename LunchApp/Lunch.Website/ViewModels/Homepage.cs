using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lunch.Core.Models;

namespace Lunch.Website.ViewModels
{
    public class Homepage
    {

        public List<Core.Models.RestaurantOption> RestaurantsForToday { get; set; }
        public List<Core.Models.User> PeopleWhoVotedToday { get; set; }
        public Vote YourVote { get; set; }
        public RestaurantRating YourRating { get; set; }
        public RestaurantOption WinningRestaurant { get; set; }

        //if you have overrides or vetos????
    }
}