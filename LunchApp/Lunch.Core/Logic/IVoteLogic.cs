using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Models;

namespace Lunch.Core.Logic
{
    public interface IVoteLogic
    {
        Vote SaveVote(Vote entity);
    }
}
