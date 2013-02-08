using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class VoteLogic : IVoteLogic
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        public VoteLogic(IVoteRepository voteRepository, IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            if (voteRepository == null) throw new ArgumentNullException("voteRepository");
            if (restaurantRepository == null) throw new ArgumentNullException("restaurantRepository");
            if (userRepository == null) throw new ArgumentNullException("userRepository");
            _voteRepository = voteRepository;
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        public Vote GetItem(int id)
        {
            return _voteRepository.GetItem(id);
        }

        public Vote GetItem(int userID, DateTime date)
        {
            return _voteRepository.GetItem(userID, date);
        }

        public IList<Vote> GetItemsByUser(int userID)
        {
            return _voteRepository.GetItemsByUser(userID);        }

        public IList<Vote> GetItemsByRestaurant(int restaurantID)
        {
            return _voteRepository.GetItemsByRestaurant(restaurantID);        }

        public IList<Vote> GetItemsByRestaurant(int restaurantID, DateTime date)
        {
            return _voteRepository.GetItemsByRestaurant(restaurantID, date);        }

        public IList<Vote> GetItemsByMonthAndYear(int month, int year)
        {
            return _voteRepository.GetItemsByMonthAndYear(month, year);
        }

        public IList<Vote> GetItemsForDate(DateTime? date)
        {
            if (date == null) date = DateTime.Now;
            return _voteRepository.GetListForDate(date.Value);
        }

        public Vote SaveVote(Vote entity)
        {
            if (entity == null || entity.UserId == 0 || entity.RestaurantId == 0) return entity;
            var match = _voteRepository.GetItem(entity.UserId, DateTime.Now);
            if (match == null)
            {
                entity.VoteDate = DateTime.Now;
                entity = _voteRepository.SaveOrUpdate(entity);
            }
            return entity;
        }

        public Vote SaveVote(int restaurantID, int userID)
        {
            var entity = new Vote {RestaurantId = restaurantID, UserId = userID};
            return SaveVote(entity);
        }

        public IDictionary<int, int> GetRestaurantMonthlyVoteCount()
        {
            var results = new Dictionary<int, int>();
            var restaurants = _restaurantRepository.GetList(null);
            if (restaurants != null)
            {
                var votes = _voteRepository.GetItemsByMonthAndYear(DateTime.Now.Month, DateTime.Now.Year);
                foreach (var restaurant in restaurants.ToList())
                {
                    if (!results.ContainsKey(restaurant.Id))
                    {
                        var count = votes.Count(f => f.RestaurantId == restaurant.Id);
                        results.Add(restaurant.Id, count);
                    }
                }
            }
            return results;
        }

        public IDictionary<int, int> GetUserMonthlyVoteCount()
        {
            var results = new Dictionary<int, int>();
            var users = _userRepository.GetList(null);
            if (users != null)
            {
                var votes = _voteRepository.GetItemsByMonthAndYear(DateTime.Now.Month, DateTime.Now.Year);
                foreach (var user in users.ToList())
                {
                    if (!results.ContainsKey(user.Id))
                    {
                        var count = votes.Count(f => f.UserId == user.Id);
                        results.Add(user.Id, count);
                    }
                }
            }
            return results;
        }

        public void Delete(int id)
        {
            _voteRepository.Delete(id);
        }
    }
}
