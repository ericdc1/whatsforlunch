using System;

namespace Lunch.Core.Models
{
    public class Job
    {
        public virtual int ID { get; protected set; }
        public virtual string MethodName { get; set; }
        public virtual string ParametersJson { get; set; }
        public virtual DateTime RunDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}