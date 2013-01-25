using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("Holidays")]
    public class Holiday : Template.Holiday
    {
        public override int ID { get; set; }
        public override DateTime ExcludedDate { get; set; }
    }

}
