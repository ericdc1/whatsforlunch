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

    public class Job : Database.Job
    {

    }
}