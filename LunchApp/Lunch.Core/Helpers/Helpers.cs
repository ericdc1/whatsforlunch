using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.Models;
using StructureMap;

namespace Lunch.Core.Helpers
{
    public class Helpers
    {

        public static bool IsLunchDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            //TODO: add checks for override dates for holidays

            return true;
        }

        public static bool SendMail(string toAddress, string fromAddress, string subject, string body)
        {

            try
            {
                var mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(toAddress));

                // From
                mailMsg.From = new MailAddress(fromAddress);

                // Subject and multipart/alternative Body
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;

                // Init SmtpClient and send
                var smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                var credentials = new System.Net.NetworkCredential("azure_15781ccf35dcb3784a8bbde4c537d708@azure.com", "rb8t8vev");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void CreateJobs()
        {
            CreateMorningMailJob();
            CreateVotingIsOverJob();
           // CreateWhereAreWeGoingJob();
        }

        private void CreateWhereAreWeGoingJob()
        {
            //TODO:insert into db this job
            throw new NotImplementedException();
        }

        private void CreateVotingIsOverJob()
        {
            var job = new Job() { CreatedDate = DateTime.Now, MethodName = "VotingIsOverMessage", ParametersJson = "{name:bob}", RunDate = Helpers.AdjustTimeOffsetToUtc(DateTime.Today.AddHours(12)) };
            var _jobLogic = ObjectFactory.GetInstance<IJobLogic>();
            _jobLogic.SaveOrUpdate(job);

            //add log
            var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
            var entity = new JobLog() { JobID = 0, Category = "System", Message = "Create voting is over mail job" };
            _jobLogLogic.SaveOrUpdate(entity);
        }

        private void CreateMorningMailJob()
        {
            var job = new Job() { CreatedDate = DateTime.Now, MethodName = "MorningMessage", ParametersJson = "{name:bob}", RunDate = Helpers.AdjustTimeOffsetToUtc(DateTime.Today.AddHours(7))};
            var _jobLogic = ObjectFactory.GetInstance<IJobLogic>();
            _jobLogic.SaveOrUpdate(job);

            //add log
            var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
            var entity = new JobLog() { JobID = 0, Category = "System", Message = "Create morning mail job" };
            _jobLogLogic.SaveOrUpdate(entity);
        }

        public void RunJob(string methodname, string parameters, int id)
        {
            var calledType = Type.GetType("Lunch.Core.Helpers.Jobs");
            if (calledType != null)
            {
                var methods = calledType.GetMethods(BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public);
                foreach (var method in methods)
                {
                    if (method.Name == methodname)
                    {
                        //object jobsInstance =  Activator.CreateInstance(calledType);
                        object jobsInstance = ObjectFactory.GetInstance(calledType);
                        calledType.InvokeMember(methodname, BindingFlags.InvokeMethod| BindingFlags.Public | BindingFlags.Instance, null, jobsInstance, new object[] { parameters, id });
                        break;
                    }
                }
            }
            else
            {
                //add log
                var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
                var entity = new JobLog() { JobID = 0, Category = "System" , Message=string.Format("Running job {0} failed",methodname) };
                _jobLogLogic.SaveOrUpdate(entity);
            }
        }


        public static DateTime AdjustTimeOffsetFromUtc(DateTime time)
        {
            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easterntime = utc.Add(eastern.BaseUtcOffset);

            TimeSpan diff = (easterntime-utc);
            double hours = diff.TotalHours;
            return time.AddHours(hours);
        }

        public static DateTime AdjustTimeOffsetToUtc(DateTime time)
        {    
            var utc = DateTime.UtcNow;
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easterntime = utc.Add(eastern.BaseUtcOffset);

            TimeSpan diff = (utc-easterntime);
            double hours = diff.TotalHours;
            return time.AddHours(hours);
        }


        public static string Keepalive()
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

