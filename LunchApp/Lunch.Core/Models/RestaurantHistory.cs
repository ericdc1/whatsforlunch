﻿using System;

namespace Lunch.Core.Models
{
    public class RestaurantHistory
    {
        public virtual int Id { get; set; }
        public virtual int RestaurantID { get; set; }
        public virtual DateTime VisitDate { get; set; }
        public virtual SelectionMethod SelectionMethod { get; set; }
    }
    public enum SelectionMethod
    {
        Vote,
        Override
    }
}