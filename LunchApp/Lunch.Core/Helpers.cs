using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Core
{
    public static class Helpers
    {

        //public static DateTime AdjustTimeOffsetFromUtc(DateTime time)
        //{
        //    var utc = DateTime.UtcNow;
        //    var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        //    var easterntime = utc.Add(eastern.BaseUtcOffset);

        //    TimeSpan diff = (easterntime - utc);
        //    double hours = diff.TotalHours;
        //    return time.AddHours(hours);
        //}

        public static DateTime AdjustTimeOffsetFromUtc(DateTime time, string zoneName = "Eastern Standard Time")
        {

            var x = TimeZoneInfo.GetSystemTimeZones();
            var utc = DateTime.UtcNow;
            //get the current timezone info
            var zoneinfo = TimeZoneInfo.FindSystemTimeZoneById(zoneName);
            //get the daylight savings time zone name
            var daylightname = zoneinfo.DaylightName;
            //get the time info
            var timeinfo = utc.Add(zoneinfo.BaseUtcOffset);
            //find out if the current time is in DST range
            var isDaylight = zoneinfo.IsDaylightSavingTime(timeinfo);

            //if the time is in DST range change zones to the DST zone
            if (isDaylight)
            {
                timeinfo = utc.Add(zoneinfo.BaseUtcOffset).AddHours(1);
            }

            TimeSpan diff = (timeinfo - utc);
            double hours = diff.TotalHours;
            return time.AddHours(hours);
        }


        public static DateTime AdjustTimeOffsetToUtc(DateTime time, string zoneName = "Eastern Standard Time")
        {
            var utc = DateTime.UtcNow;
            //get the current timezone info
            var zoneinfo = TimeZoneInfo.FindSystemTimeZoneById(zoneName);
            //get the daylight savings time zone name
            var daylightname = zoneinfo.DaylightName;
            //get the time info
            var timeinfo = utc.Add(zoneinfo.BaseUtcOffset);
            //find out if the current time is in DST range
            var isDaylight = zoneinfo.IsDaylightSavingTime(timeinfo);
           
            //if the time is in DST range change zones to the DST zone
            if(isDaylight)
            {
                timeinfo = utc.Add(zoneinfo.BaseUtcOffset).AddHours(1);
            }

            TimeSpan diff = (utc - timeinfo);
            double hours = diff.TotalHours;
            return time.AddHours(hours);
        }
    }
}
