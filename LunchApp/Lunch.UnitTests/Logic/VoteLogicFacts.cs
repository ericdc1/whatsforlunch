using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.RepositoryInterfaces;
using Moq;

namespace Lunch.UnitTests.Logic
{
    public class VoteLogicFacts
    {
        private IVoteLogic _voteLogic;
        private Mock<IVoteRepository> _mockVoteRepository;

        public VoteLogicFacts()
        {
            _mockVoteRepository = new Mock<IVoteRepository>();
            _voteLogic = new VoteLogic(_mockVoteRepository.Object);
        }

        public class SaveVote : VoteLogicFacts
        {
            [Test]
            public void IfModelIsNullReturnsModelInstance()
            {
                
            }
        }

        public void Dispose()
        {
            _voteLogic = null;
            _mockVoteRepository = null;
        }
    }
}
