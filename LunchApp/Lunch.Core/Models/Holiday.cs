using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("Holidays")]
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime ExcludedDate { get; set; }
    }

}
