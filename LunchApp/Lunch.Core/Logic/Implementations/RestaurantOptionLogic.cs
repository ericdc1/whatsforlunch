using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantOptionLogic : IRestaurantOptionLogic 
    {
        private readonly IRestaurantOptionRepository _restaurantOptionRepository;
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IVoteLogic _voteLogic;

        public RestaurantOptionLogic(IRestaurantOptionRepository restaurantOptionRepository, IRestaurantLogic restaurantLogic, IVoteLogic voteLogic)
        {
            _restaurantOptionRepository = restaurantOptionRepository;
            _restaurantLogic = restaurantLogic;
            _voteLogic = voteLogic;
        }


        public IEnumerable<RestaurantOption> GetAll()
        {
            var ratings = _restaurantOptionRepository.GetAll();

            return ratings;
        }
        
        public IEnumerable<RestaurantOption> GetAllByDate(DateTime? dateTime)
        {
            if (dateTime == null) dateTime = DateTime.Now;

            return _restaurantOptionRepository.GetAllByDate(dateTime.Value);
        }
        
        public IEnumerable<RestaurantOption> GetAndSaveOptions()
        {
            var options = GetAllByDate(DateTime.Now).ToList();

            if (!options.Any())
            {
                options = new List<RestaurantOption>();
                var restaurants = _restaurantLogic.GetSelection().ToList();
                foreach (var restaurant in restaurants)
                {
                    var option = new RestaurantOption {RestaurantId = restaurant.Id, Selected = 0, SelectedDate = DateTime.Now};
                    option.Restaurant = restaurant;
                    options.Add(option);
                    SaveOrUpdate(option);
                }
            }
            
            return options;
        }
        
        public RestaurantOption FinalizeOptions()
        {
            var options = GetAllByDate(DateTime.Now).ToList();
            var votes = _voteLogic.GetItemsForDate(null);

            foreach (var option in options)
                option.Votes = votes.Count(v => v.RestaurantId == option.RestaurantId);

            var selectedRestaurant = options.OrderByDescending(o => o.Votes).FirstOrDefault();

            if (selectedRestaurant != null)
            {
                selectedRestaurant.Selected = 1;
                _restaurantOptionRepository.SaveOrUpdate(selectedRestaurant);
            }
                
            return selectedRestaurant;
        }


        public RestaurantOption SaveOrUpdate(RestaurantOption entity)
        {
            return _restaurantOptionRepository.SaveOrUpdate(entity);
        }
    }
}
