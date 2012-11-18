using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.Models
{
    public class JobLog
    {
        public int ID { get; set; }
        public DateTime LogDTM { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
    }
}