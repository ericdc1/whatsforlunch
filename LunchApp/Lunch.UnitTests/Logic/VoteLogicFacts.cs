using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Moq;
using Xunit;

namespace Lunch.UnitTests.Logic
{
    public class VoteLogicFacts
    {
        private IVoteLogic _voteLogic;
        private Mock<IVoteRepository> _mockVoteRepository;
        private Mock<IRestaurantRepository> _mockRestaurantRepository;
        private Mock<IUserRepository> _mockUserRepository; 

        public VoteLogicFacts()
        {
            _mockVoteRepository = new Mock<IVoteRepository>();
            _voteLogic = new VoteLogic(_mockVoteRepository.Object, _mockRestaurantRepository.Object, _mockUserRepository.Object);
        }

        public class SaveVote : VoteLogicFacts
        {
            [Fact]
            public void IfModelIsNullReturnsModelInstance()
            {
                var model = (Vote) null;
            }
        }

        public void Dispose()
        {
            _voteLogic = null;
            _mockVoteRepository = null;
            _mockRestaurantRepository = null;
            _mockUserRepository = null;
        }
    }
}
