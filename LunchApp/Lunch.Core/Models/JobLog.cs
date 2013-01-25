using System;
using System.ComponentModel;

namespace Lunch.Core.Models
{
    /// <summary>
    /// Defines business model entites which are connected/dependent on JobLog model.
    /// </summary>
    [Flags]
    public enum JobLogDependencies
    {
        Job = 1
    }

    public class JobLog : Template.JobLog
    {
        public override int Id { get;  set; }
        [DisplayName("Job ID")]
        public override int? JobID { get; set; }
        [DisplayName("Log Date")]
        public override DateTime LogDTM { get; set; }
        public override string Category { get; set; }
        public override string Message { get; set; }
        public virtual Job Job { get; set; }


    }
}