﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.Models.Views;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class RestaurantLogic : IRestaurantLogic
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantRatingLogic _restaurantRatingLogic;
        private readonly IVoteLogic _voteLogic;

        public RestaurantLogic(IRestaurantRepository restaurantRepository, IRestaurantRatingLogic restaurantRatingLogic, IVoteLogic voteLogic)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantRatingLogic = restaurantRatingLogic;
            _voteLogic = voteLogic;
        }


        public IEnumerable<Restaurant> GetTop(int count = 10)
        {
            var restaurants = _restaurantRepository.GetList(new {}).ToList();
            var ratings = _restaurantRatingLogic.GetAll().ToList();
            var userVotes = _voteLogic.GetUserMonthlyVoteCount();
            var restaurantVotes = _voteLogic.GetRestaurantMonthlyVoteCount();

            // put in some temp data
            //_voteLogic.SaveVote(3, 37);
            //_voteLogic.SaveVote(8, 38);
            //_voteLogic.SaveVote(9, 39);

            // multiply each users ratings by their vote weight
            var userVoteMax = userVotes.OrderByDescending(v => v.Value).First().Value;
            if (userVoteMax > 0)
                foreach (var i in userVotes)
                {
                    var userVote = i;
                    var userRatings = ratings.Where(r => r.UserId == userVote.Key);
                    foreach (var userRating in userRatings)
                    {
                        userRating.Rating = userRating.Rating * (userVote.Value / userVoteMax);
                    }
                }

            // multiple each restaurants rating by their vote weight
            var restaurantVoteMax = restaurantVotes.OrderByDescending(v => v.Value).First().Value;
            if (restaurantVoteMax > 0)
                foreach (var i in restaurantVotes)
                {
                    var restaurantVote = i;
                    var restaurantRatings = ratings.Where(r => r.RestaurantId == restaurantVote.Key);
                    foreach (var restaurantRating in restaurantRatings)
                    {
                        restaurantRating.Rating = restaurantRating.Rating * (restaurantVote.Value / restaurantVoteMax);
                    }
                }

            // for each restaurant find the average
            foreach (var restaurant in restaurants)
            {
                var restaurantRatings = ratings.Where(r => r.RestaurantId == restaurant.Id).ToList();
                restaurant.Rating = (double)restaurantRatings.Sum(r => r.Rating) / (double)restaurantRatings.Count();
            }

            return restaurants.Take(count);
        }

        public IEnumerable<Restaurant> GetSelection()
        {
            var selection = new List<Restaurant>();

            var topRestaurants = GetTop().ToList();
            var allRestaurants = GetList(new {}).ToList();

            // TODO: remove those picked recently

            var random = new Random();

            // get 2 randomly from the top 10 (less the ones removed by visit date)
            selection.AddRange(topRestaurants.OrderBy(x => random.Next()).Take(2));

            // get 2 randomly from all restaurants (less the ones already in the selection)
            allRestaurants.RemoveAll(m => m.Id == selection[0].Id || m.Id == selection[1].Id);
            selection.AddRange(allRestaurants.OrderBy(x => random.Next()).Take(2));
            allRestaurants.RemoveAll(m => m.Id == selection[2].Id || m.Id == selection[3].Id);

            var specialRestaurants = allRestaurants.Where(r => r.PreferredDayOfWeek != null);

            var specialRestaurant = specialRestaurants.OrderBy(x => random.Next()).Take(1).FirstOrDefault();
            if (specialRestaurant != null)
                selection.Add(specialRestaurant);

            return selection;
        } 

        public IEnumerable<Restaurant> GetList(object parameters)
        {
            return _restaurantRepository.GetList(parameters);
        }

        public IEnumerable<Restaurant> GenerateRestaurants()
        {
            return _restaurantRepository.GetList(new{}).OrderBy (x => Guid.NewGuid()).Take(4);
        }

        public IEnumerable<RestaurantDetails> GetAllDetailed(int? categoryId)
        {
            return _restaurantRepository.GetAllDetailed(categoryId);
        }

        public Restaurant Get(int id)
        {
            return _restaurantRepository.Get(id);
        }

        public IEnumerable<Restaurant> SaveOrUpdateAll(params Restaurant[] entities)
        {
            return _restaurantRepository.SaveOrUpdateAll(entities);
        }

        public Restaurant SaveOrUpdate(Restaurant entity)
        {
            return _restaurantRepository.SaveOrUpdate(entity);
        }

        public Restaurant Delete(Restaurant entity)
        {
            return _restaurantRepository.Delete(entity);
        }
    }
}
