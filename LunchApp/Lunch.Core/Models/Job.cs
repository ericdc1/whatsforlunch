using System;
using System.Collections.Generic;

namespace Lunch.Core.Models
{
    /// <summary>
    /// Defines business model entites which are connected/dependent on Job model.
    /// </summary>
    [Flags]
    public enum JobDependencies
    {
        JobLogs = 1
    }

    public class Job
    {
        public virtual int ID { get; protected set; }
        public virtual string MethodName { get; set; }
        public virtual string ParametersJson { get; set; }
        public virtual DateTime RunDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual IList<JobLog> JobLogs { get; set; }
        public virtual bool HasRun { get; set; }
    }
}