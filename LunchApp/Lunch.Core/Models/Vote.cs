using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Lunch.Core.Models
{
    [Table("Votes")]
    public class Vote
    {
        public virtual int Id { get; set; }
        public virtual int UserID { get; set; }
        public virtual int RestaurantID { get; set; }
        public virtual DateTime? VoteDate { get; set; }
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}