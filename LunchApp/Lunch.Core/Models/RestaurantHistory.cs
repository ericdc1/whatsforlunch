using System;

namespace Lunch.Core.Models
{
    public class RestaurantHistory
    {
        public virtual SelectionMethod SelectionMethod { get; set; }
    }
    public enum SelectionMethod
    {
        Vote,
        Override
    }
}