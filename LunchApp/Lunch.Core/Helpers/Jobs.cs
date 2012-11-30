using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lunch.Core.Helpers
{
    public class Jobs
    {
        public static void Test1(object model)
        {
            keepalive();
            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var sb = new StringBuilder();
            sb.Append("<li>Local time" + DateTime.Now);
            sb.Append("<li>Eastern time" + utc.Add(eastern.BaseUtcOffset));
            //new Models.JobLogRepository().Insert(new JobLog { Category = "test1", LogDTM = DateTime.Now, Message = "Job running - " + sb.ToString() });
        }
        public static void Test2(object model)
        {
            keepalive();
            //new Models.JobLogRepository().Insert(new JobLog { Category = "test2", LogDTM = DateTime.Now, Message = "Job running" });

        }
        public static void Test3(object model)
        {
            keepalive();
            //new Models.JobLogRepository().Insert(new JobLog { Category = "test3", LogDTM = DateTime.Now, Message = "Job running" });
        }


        static string keepalive()
        {
            const string url = "http://whatsforlunch.azurewebsites.net/home/keepalive";
            String strResult;
            var objRequest = WebRequest.Create(url);
            var objResponse = objRequest.GetResponse();
            using (var sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }

    }
}