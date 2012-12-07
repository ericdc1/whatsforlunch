using System.Collections.Generic;

namespace Lunch.Core.Models
{
    public class RestaurantType
    {
        public virtual int ID { get; protected set; }
        public virtual string TypeName { get; set; }
        public virtual IList<Restaurant> Restaurants { get; set; }
    }
}
