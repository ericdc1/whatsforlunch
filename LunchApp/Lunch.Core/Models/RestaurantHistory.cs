using System;

namespace Lunch.Core.Models
{
    public class RestaurantHistory
    {
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public DateTime VisitDate { get; set; }
        public SelectionMethod SelectionMethod { get; set; }
    }
    public enum SelectionMethod
    {
        Vote,
        Override
    }
}

//ToDo: Create logging inserts