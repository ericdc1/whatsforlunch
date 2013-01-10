using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Xipton.Razor;

namespace Lunch.Core.Jobs
{
    public class Jobs
    {
        private readonly IJobLogLogic _jobLogLogic;
        private readonly IUserLogic _userLogic;
        private readonly IRestaurantLogic _restaurantLogic;



        public Jobs(IJobLogLogic jobLogLogic, IUserLogic userLogic, IRestaurantLogic restaurantLogic)
        {
            _jobLogLogic = jobLogLogic;
            _userLogic = userLogic;
            _restaurantLogic = restaurantLogic;
        }

        public void MorningMessage(object model, int id)
        {
            var peopletoreceivemail = _userLogic.GetList(new { SendMail1 = true });
            var todayschoices = _restaurantLogic.GenerateRestaurants().ToList();
            var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");
            var rm = new RazorMachine();

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail)
            {
                //var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                //string text = System.IO.File.ReadAllText(new Uri(path + "/mail.cshtml").AbsolutePath);
                //ITemplate template = rm.ExecuteContent(text, new { FirstName = "Eric", GUID = "wewewewqewqeq" },null,true);
                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?GUID={1}", baseurl, user.GUID);
                ITemplate template = rm.ExecuteUrl("~/_MailTemplates/Morning.cshtml", new MailDetails { User = user, Restaurants = todayschoices, Url = link }, null, true);

                Core.Jobs.Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", template.Result);

                //add log
                var entity = new JobLog() { JobID = id, Category = "MorningMessage", Message = string.Format("Morning message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }

        public void VotingIsOverMessage(object model, int id)
        {
            var peopletoreceivemail = _userLogic.GetList(new { SendMail2 = true });
            var todayschoices = _restaurantLogic.GenerateRestaurants().Take(2).ToList();
            var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");
            var rm = new RazorMachine();

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail)
            {
                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?GUID={1}", baseurl, user.GUID);
                ITemplate template = rm.ExecuteUrl("~/_MailTemplates/VotingOver.cshtml", new MailDetails { User = user, Restaurants = todayschoices, Url = link }, null, true);

                Core.Jobs.Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", template.Result);

                //add log
                var entity = new JobLog() { JobID = id, Category = "VotingIsOverMessage", Message = string.Format("Voting is over message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }


        public void WhereAreWeGoingMessage(object model, int id)
        {
            var peopletoreceivemail = _userLogic.GetList(new { SendMail3 = true });
            var todayschoices = _restaurantLogic.GenerateRestaurants().Take(1).ToList();
            var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");
            var rm = new RazorMachine();

            //send email to each person who is eligible
            foreach (var user in peopletoreceivemail)
            {
                var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
                var link = string.Format("{0}?GUID={1}", baseurl, user.GUID);
                ITemplate template = rm.ExecuteUrl("~/_MailTemplates/WhereGoing.cshtml", new MailDetails { User = user, Restaurants = todayschoices, Url = link }, null, true);

                Core.Jobs.Helpers.SendMail(user.Email, fromaddress, "What's for Lunch Message of the day", template.Result);

                //add log
                var entity = new JobLog() { JobID = id, Category = "VotingIsOverMessage", Message = string.Format("Where are we going message sent to {0}", user.FullName) };
                _jobLogLogic.SaveOrUpdate(entity);
            }

        }

    }
}