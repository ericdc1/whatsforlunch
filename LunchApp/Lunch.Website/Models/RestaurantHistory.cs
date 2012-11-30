using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.Models
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