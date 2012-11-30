using System;

namespace Lunch.Core.Models
{
    public class JobLog
    {
        public virtual int ID { get; protected set; }
        public virtual DateTime LogDTM { get; set; }
        public virtual string Category { get; set; }
        public virtual string Message { get; set; }
    }
}