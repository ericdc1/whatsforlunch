using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace Lunch.Core.Models
{
    [Table("Votes")]
    public class Vote : Database.Vote
    {
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}