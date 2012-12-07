using System;

namespace Lunch.Core.Models
{
    public class Vote
    {
        public virtual int ID { get; protected set; }
        public virtual DateTime VoteDate { get; set; }
        public virtual int UserID { get; set; }
        public virtual int RestaurantID { get; set; }
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}