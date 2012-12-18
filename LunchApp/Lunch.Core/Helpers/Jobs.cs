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

        public void MorningMessage(object model, int id)
        {
            keepalive();
          
            //TODO: populate from query of users who have morning mail flag set
            var peopletoreceivemail = new List<User>();
            peopletoreceivemail.Add(new User() {FullName="Eric Coffman", Email="ecoffman@hsc.wvu.edu", SendMorningEmailFlg=true, GUID =  Guid.NewGuid().ToString()});
            peopletoreceivemail.Add(new User() { FullName = "Libby DeHaan", Email = "edehaan@hsc.wvu.edu", SendMorningEmailFlg = false });
            peopletoreceivemail.Add(new User() { FullName = "No One", Email = "noone@hsc.wvu.edu", SendMorningEmailFlg = false });

            var todayschoices = new List<Restaurant>();

            int numberofpeople = peopletoreceivemail.Count;

            //ToDo: set in web.config
            var fromaddress = "admin@lunchapp.com";

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail.Where(f=>f.SendMorningEmailFlg))
            {    
                var messagesb = new StringBuilder();
                messagesb.Append("Today's choices are");
                foreach (var restaurant in todayschoices)
                {
                    messagesb.Append(restaurant.RestaurantName);                    
                }

                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?u={1}", baseurl, user.GUID);
                messagesb.Append(string.Format("Click here to vote - <a href='{0}'>Login</a>", link));

                Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", messagesb.ToString()); 
    
                //add log
               var entity = new JobLog() {JobID= id, Category="MorningMessage", Message=string.Format("Morning message sent to {0}", user.FullName)};
               _jobLogLogic.SaveOrUpdate(entity);
            }
           
        }


        public void VotingIsOverMessage(object model, int id)
        {
            keepalive();

            //TODO: populate from query of users who have morning mail flag set
            var peopletoreceivemail = new List<User>();
            peopletoreceivemail.Add(new User() { FullName = "Eric Coffman", Email = "ecoffman@hsc.wvu.edu", SendMorningEmailFlg = true, GUID = Guid.NewGuid().ToString() });
            peopletoreceivemail.Add(new User() { FullName = "Libby DeHaan", Email = "edehaan@hsc.wvu.edu", SendMorningEmailFlg = false });
            peopletoreceivemail.Add(new User() { FullName = "No One", Email = "noone@hsc.wvu.edu", SendMorningEmailFlg = false });

            var todayschoices = new List<Restaurant>();

            int numberofpeople = peopletoreceivemail.Count;

            //ToDo: set in web.config
            var fromaddress = "admin@lunchapp.com";

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail.Where(f => f.SendMorningEmailFlg))
            {
                var messagesb = new StringBuilder();
                messagesb.Append("Voting is over");
                foreach (var restaurant in todayschoices)
                {
                    messagesb.Append(restaurant.RestaurantName);
                }

                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?u={1}", baseurl, user.GUID);
                messagesb.Append(string.Format("Click here to view results - <a href='{0}'>Login</a>", link));

                Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Voting is over", messagesb.ToString());

                //add log
                var entity = new JobLog() { JobID = id, Category = "VotingIsOverMessage", Message = string.Format("Voting is over message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }


        static string keepalive()
        {
            var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
            string url = string.Format("{0}/home/keepalive", baseurl);
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