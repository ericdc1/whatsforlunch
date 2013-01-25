using System;
using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class VoteLogic : IVoteLogic
    {
        private readonly IVoteRepository _voteRepository;

        public VoteLogic(IVoteRepository voteRepository)
        {
            if (voteRepository == null) throw new ArgumentNullException("voteRepository", "Value cannot be null.");
            _voteRepository = voteRepository;
        }

        public Vote GetItem(int id)
        {
            return _voteRepository.GetItem(id);
        }

        public Vote GetItem(int userID, DateTime? date)
        {
            return _voteRepository.GetItem(userID, date);
        }

        public IList<Vote> GetItemsByUser(int userID)
        {
            return _voteRepository.GetItemsByUser(userID);        }

        public IList<Vote> GetItemsByRestaurant(int restaurantID)
        {
            return _voteRepository.GetItemsByRestaurant(restaurantID);        }

        public IList<Vote> GetItemsByRestaurant(int restaurantID, DateTime? date)
        {
            return _voteRepository.GetItemsByRestaurant(restaurantID, date);        }

        public Vote SaveVote(Vote entity)
        {
            if (entity == null || entity.UserID == 0 || entity.RestaurantID == 0) return entity;
            var match = _voteRepository.GetItem(entity.UserID, DateTime.Now);
            if (match == null)
            {
                entity.VoteDate = DateTime.Now;
                entity = _voteRepository.SaveOrUpdate(entity);
            }
            return entity;
        }

        public Vote SaveVote(int restaurantID, int userID)
        {
            var entity = new Vote {RestaurantID = restaurantID, UserID = userID};
            return SaveVote(entity);
        }
    }
}
