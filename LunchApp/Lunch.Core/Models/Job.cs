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

    public class Job : Template.Job
    {
        public override int Id { get;  set; }
        [DisplayName("Method Name")]
        public override string MethodName { get; set; }
        [DisplayName("Parameters")]
        public override string ParametersJson { get; set; }
        [DisplayName("Run Date")]
        public override DateTime RunDate { get; set; }
        [DisplayName("Created Date")]
        public override DateTime CreatedDate { get; set; }
        public virtual IList<JobLog> JobLogs { get; set; }
        [DisplayName("Has Run")]
        public override bool HasRun { get; set; }
    }
}