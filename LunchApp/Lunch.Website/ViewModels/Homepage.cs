using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lunch.Core.Models;

namespace Lunch.Website.ViewModels
{
    public class Homepage
    {

        public List<Restaurant> RestaurantsForToday { get; set; }
        public List<User> PeopleWhoVotedToday { get; set; }
        public Vote YourVote { get; set; }

        //if you have overrides or vetos????
    }
}