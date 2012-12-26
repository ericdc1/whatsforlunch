using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public virtual int Id { get;  set; }
        [DisplayName("Method Name")]
        public virtual string MethodName { get; set; }
        [DisplayName("Parameters")]
        public virtual string ParametersJson { get; set; }
        [DisplayName("Run Date")]
        public virtual DateTime RunDate { get; set; }
        [DisplayName("Created Date")]
        public virtual DateTime CreatedDate { get; set; }
        public virtual IList<JobLog> JobLogs { get; set; }
        [DisplayName("Has Run")]
        public virtual bool HasRun { get; set; }
    }
}