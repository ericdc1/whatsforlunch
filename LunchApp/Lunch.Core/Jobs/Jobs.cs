﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using RazorEngine;


namespace Lunch.Core.Jobs
{
    public class Jobs
    {
        private readonly IJobLogLogic _jobLogLogic;
        private readonly IUserLogic _userLogic;
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantOptionLogic _restaurantOptionLogic;



        public Jobs(IJobLogLogic jobLogLogic, IUserLogic userLogic, IRestaurantLogic restaurantLogic, IRestaurantOptionLogic restaurantOptionLogic)
        {
            _jobLogLogic = jobLogLogic;
            _userLogic = userLogic;
            _restaurantLogic = restaurantLogic;
            _restaurantOptionLogic = restaurantOptionLogic;
        }

        public void MorningMessage(object model, int id)
        {
            var peopletoreceivemail = _userLogic.GetList(new {SendMail1 = true});
            var todayschoices = _restaurantOptionLogic.GetAndSaveOptions().ToList();
            var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail)
            {
                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?GUID={1}", baseurl, user.Guid);
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                var template = File.ReadAllText(new Uri(path + "/Views/_MailTemplates/Morning.cshtml").AbsolutePath);
                var messagemodel = new MailDetails() {User = user, Restaurants = todayschoices, Url = link};

                string result = Razor.Parse(template, messagemodel);
                Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", result);

                //add log
                var entity = new JobLog() { JobId = id, Category = "MorningMessage", Message = string.Format("Morning message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }

        public void VotingIsOverMessage(object model, int id)
        {
            var peopletoreceivemail = _userLogic.GetList(new { SendMail2 = true });
            var todayschoices = _restaurantOptionLogic.GetAllByDate(null).OrderBy(f => f.Votes).Take(2).ToList();
            var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");


            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail)
            {
                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?GUID={1}", baseurl, user.Guid);

                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                var template = File.ReadAllText(new Uri(path + "/Views/_MailTemplates/Morning.cshtml").AbsolutePath);
                var messagemodel = new MailDetails() { User = user, Restaurants = todayschoices, Url = link };
                string result = Razor.Parse(template, messagemodel);
                Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Voting Is Over", result);

                //add log
                var entity = new JobLog() { JobId = id, Category = "VotingIsOverMessage", Message = string.Format("Voting is over message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }


        public void WhereAreWeGoingMessage(object model, int id)
        {
            var peopletoreceivemail = _userLogic.GetList(new { SendMail3 = true });
            var todayschoices = _restaurantOptionLogic.GetAllByDate(null).OrderBy(f => f.Votes).Take(1).ToList();
            var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");


            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail)
            {
                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?GUID={1}", baseurl, user.Guid);

                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                var template = File.ReadAllText(new Uri(path + "/Views/_MailTemplates/WhereGoing.cshtml").AbsolutePath);
                var messagemodel = new MailDetails() { User = user, Restaurants = todayschoices, Url = link };
                string result = Razor.Parse(template, messagemodel);
                Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", result);

                //add log
                var entity = new JobLog() { JobId= id, Category = "VotingIsOverMessage", Message = string.Format("Where are we going message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }

    }
}