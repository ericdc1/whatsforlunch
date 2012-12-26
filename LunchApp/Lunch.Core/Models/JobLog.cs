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

    public class JobLog
    {
        public virtual int Id { get;  set; }
        [DisplayName("Job ID")]
        public virtual int JobID { get; set; }
        [DisplayName("Log Date")]
        public virtual DateTime LogDTM { get; set; }
        public virtual string Category { get; set; }
        public virtual string Message { get; set; }
        public virtual Job Job { get; set; }


    }
}