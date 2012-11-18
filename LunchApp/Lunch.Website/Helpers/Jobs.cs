using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using Lunch.Website.Models;

namespace Lunch.Website.Helpers
{
    public class Jobs
    {
        public static void Test1()
        {

            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var sb = new StringBuilder();
            sb.Append("<li>Local time" + DateTime.Now);
            sb.Append("<li>Eastern time" + utc.Add(eastern.BaseUtcOffset));
            new Models.JobLogRepository().Insert(new JobLog { Category = "test1", LogDTM = DateTime.Now, Message = "Job running - " + sb.ToString() });
        }
        public static void Test2()
        {
            new Models.JobLogRepository().Insert(new JobLog { Category = "test2", LogDTM = DateTime.Now, Message = "Job running" });

        }
        public static void Test3()
        {
            new Models.JobLogRepository().Insert(new JobLog { Category = "test3", LogDTM = DateTime.Now, Message = "Job running" });
        }
    }
}