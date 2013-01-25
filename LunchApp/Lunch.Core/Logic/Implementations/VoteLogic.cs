using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Vote SaveVote(Vote entity)
        {

            throw new NotImplementedException();
        }
    }
}
