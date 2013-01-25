using System;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;
using Xunit;
using Moq;
using Xunit;
using Xunit.Sdk;

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
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _voteLogic = new VoteLogic(_mockVoteRepository.Object, _mockRestaurantRepository.Object, _mockUserRepository.Object);
        }

        public class SaveVote : VoteLogicFacts
        {
            [Fact]
            public void IfModelIsNullReturnsNull()
            {
                var result = _voteLogic.SaveVote(null);

                Assert.Null(result);
            }

            [Fact]
            public void IfModelUserIDIs0ReturnsModel()
            {
                var model = new Vote() {UserID = 0, RestaurantID = 1};

                var result = _voteLogic.SaveVote(model);

                Assert.Equal(result, model);
            }

            [Fact]
            public void IfModelRestaurantIDIs0ReturnsModel()
            {
                var model = new Vote { UserID = 1, RestaurantID = 0 };

                var result = _voteLogic.SaveVote(model);

                Assert.Equal(result, model);
            }

            [Fact]
            public void IfModelIsNotValidDoesNotRunVoteRepositoryGetItemMethod()
            {
                _mockVoteRepository.Setup(f => f.GetItem(It.IsAny<int>(), It.IsAny<DateTime>())).Throws(new Exception("Method should not run."));

                _voteLogic.SaveVote(null);

                _mockVoteRepository.Verify();

                _voteLogic.SaveVote(new Vote { UserID = 1, RestaurantID = 0 });

                _mockVoteRepository.Verify();

                _voteLogic.SaveVote(new Vote { UserID = 0, RestaurantID = 1 });

                _mockVoteRepository.Verify();
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
