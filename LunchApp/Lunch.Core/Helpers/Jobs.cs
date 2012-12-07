using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Core.Helpers
{
    public class Jobs
    {
        private readonly IJobLogLogic _jobLogLogic;


        public Jobs(IJobLogLogic jobLogLogic)
        {
            _jobLogLogic = jobLogLogic;
        }

        public void MorningMessage(object model)
        {
            keepalive();
            //figure out what time it really is
            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easterntime = utc.Add(eastern.BaseUtcOffset);
            var logentries = new List<JobLog>();

            //TODO: populate from query of users who have mornining mail flag set
            var peopletoreceivemail = new List<User>();
            peopletoreceivemail.Add(new User() {FullName="Eric Coffman", Email="ecoffman@hsc.wvu.edu", SendMorningEmailFlg=true, GUID =  Guid.NewGuid().ToString()});
            peopletoreceivemail.Add(new User() { FullName = "Libby DeHaan", Email = "edehaan@hsc.wvu.edu", SendMorningEmailFlg = false });
            peopletoreceivemail.Add(new User() { FullName = "No One", Email = "noone@hsc.wvu.edu", SendMorningEmailFlg = false });

            var todayschoices = new List<Restaurant>();

            int numberofpeople = peopletoreceivemail.Count;

            //ToDo: set in web.config
            var fromaddress = "admin@lunchapp.com";

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail.Where(f=>f.SendMorningEmailFlg=true))
            {    
                var messagesb = new StringBuilder();
                messagesb.Append("Today's choices are");
                foreach (var restaurant in todayschoices)
                {
                    messagesb.Append(restaurant.RestaurantName);                    
                }

                //TODO: set in web.config
                var link = string.Format("http://localhost:2227/?u={0}", user.GUID);
                messagesb.Append(string.Format("Click here to vote - <a href='{0}'>Login</a>", link));

                Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", messagesb.ToString());     
                //add log
               var entity = new JobLog() {Category="MorningMessage", LogDTM = easterntime, Message=string.Format("Morning message sent to {0}", user.FullName)};
               logentries.Add(entity);
            }

          //  _jobLogLogic.SaveOrUpdateAll(logentries.ToArray());
        }


        static string keepalive()
        {
            //TODO: set in web.config
            const string url = "http://localhost:2227/home/keepalive";
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