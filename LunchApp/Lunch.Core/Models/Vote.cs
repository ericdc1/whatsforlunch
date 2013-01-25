using System;

namespace Lunch.Core.Models
{
    public class Vote : Template.Vote
    {
        public override int Id { get; set; }
        public override int UserID { get; set; }
        public override int RestaurantID { get; set; }
        public override DateTime VoteDate { get; set; }
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}