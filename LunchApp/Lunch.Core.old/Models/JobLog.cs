﻿using System;

namespace Lunch.Core.Models
{
    public class JobLog
    {
        public int ID { get; set; }
        public DateTime LogDTM { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
    }
}