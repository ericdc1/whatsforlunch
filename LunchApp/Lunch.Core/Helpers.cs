using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Core
{
    public static class Helpers
    {

        public static DateTime AdjustTimeOffsetFromUtc(DateTime time)
        {
            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easterntime = utc.Add(eastern.BaseUtcOffset);

            TimeSpan diff = (easterntime - utc);
            double hours = diff.TotalHours;
            return time.AddHours(hours);
        }

        public static DateTime AdjustTimeOffsetToUtc(DateTime time)
        {
            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var easterntime = utc.Add(eastern.BaseUtcOffset);
            //var isDaylight = eastern.IsDaylightSavingTime(easterntime);

            TimeSpan diff = (utc - easterntime);
            double hours = diff.TotalHours;
            return time.AddHours(hours);
        }
    }
}
