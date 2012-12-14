using System;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.Models;

namespace Lunch.Website.ViewModels
{
    public class CreateRestaurant
    {
        public CreateRestaurant()
        {
        }

        public CreateRestaurant(Restaurant entity)
        {
            RestaurantName = entity.RestaurantName;
            PreferredDayOfWeek = entity.PreferredDayOfWeek;
            RestaurantTypeID = entity.RestaurantTypeID;
        }

        #region persisted
            public string RestaurantName { get; set; }
            public DayOfWeek? PreferredDayOfWeek { get; set; }
            public int RestaurantTypeID { get; set; }
        #endregion

        #region relationships
            
        #endregion 
        
        public Restaurant ToDomainModel(IRestaurantTypeLogic restaurantTypeLogic)
        {
            var entity = new Restaurant
                             {
                                 RestaurantName = RestaurantName,
                                 PreferredDayOfWeek = PreferredDayOfWeek,
                                 RestaurantTypeID = RestaurantTypeID,
                                 RestaurantType = restaurantTypeLogic.Load(RestaurantTypeID)
                             };

            return entity;
        }
    }
}