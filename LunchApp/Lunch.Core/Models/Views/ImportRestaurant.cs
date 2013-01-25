using System.Collections.Generic;

namespace Lunch.Core.Models.Views
{
    public class ImportRestaurant
    {
        private IList<RestaurantToImport> _restaurants = new List<RestaurantToImport>();

        public int TotalResults { get; set; }
        public int Page { get; set; }
        public int FirstHit { get; set; }
        public int LastHit { get; set; }
        public IList<RestaurantToImport> Restaurants
        {
            get { return _restaurants; }
            set { _restaurants = value; }
        }
    }

    public class RestaurantToImport : Restaurant
    {
        public bool Selected { get; set; }
        public bool AlreadyImported { get; set; }

        public Restaurant ToDomainModel()
        {
            var entity = new Restaurant
            {
                RestaurantName = RestaurantName,
                RestaurantTypeId = RestaurantTypeId,
                PreferredDayOfWeek = PreferredDayOfWeek
            };
            return entity;
        }
    }
}
