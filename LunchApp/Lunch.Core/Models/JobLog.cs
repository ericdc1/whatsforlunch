using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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

    public class JobLog : Database.JobLog
    {

        public virtual Job Job { get; set; }

    }
}