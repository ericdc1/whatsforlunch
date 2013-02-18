using System.Collections.Generic;
using Lunch.Core.Models;

namespace Lunch.Core.Jobs
{
    public class MailDetails
    {
        public User User { get; set; }
        public IEnumerable<RestaurantOption> Restaurants { get; set; }
        public string Url { get; set; }
        public string BaseUrl { get; set; }
    }
}