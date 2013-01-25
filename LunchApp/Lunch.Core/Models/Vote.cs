using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Lunch.Core.Models
{
    [Table("Votes")]
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