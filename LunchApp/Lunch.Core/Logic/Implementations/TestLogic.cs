using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class TestLogic : ITestLogic
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public TestLogic(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }


        public IList<Restaurant> GetRestaurants()
        {
            _restaurantRepository.SaveOrUpdate(new Restaurant() { LastVisitedDate = DateTime.Now, RestaurantName = "test" });

            return _restaurantRepository.GetAll().ToList();
        }
    }
}
