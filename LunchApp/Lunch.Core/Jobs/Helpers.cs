using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using StructureMap;

namespace Lunch.Core.Jobs
{
    public class Helpers
    {

        public static bool IsLunchDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            var holidayLogic = ObjectFactory.GetInstance<IHolidayLogic>();

            return holidayLogic.GetAll().FirstOrDefault(f =>f.ExcludedDate.ToShortDateString() == date.ToShortDateString()) == null;
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
            CreateWhereAreWeGoingJob();
        }

        private void CreateWhereAreWeGoingJob()
        {
            var job = new Job() { CreatedDate = DateTime.Now, MethodName = "WhereAreWeGoingMessage", ParametersJson = "{}", RunDate = Core.Helpers.AdjustTimeOffsetToUtc(DateTime.Today.AddHours(11).AddMinutes(15)) };
            var _jobLogic = ObjectFactory.GetInstance<IJobLogic>();
            _jobLogic.SaveOrUpdate(job);

            //add log
            var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
            var entity = new JobLog() { JobId = 0, Category = "System", Message = "Create where are we going is over mail job" };
            _jobLogLogic.SaveOrUpdate(entity);
        }

        private void CreateVotingIsOverJob()
        {
            var job = new Job() { CreatedDate = DateTime.Now, MethodName = "VotingIsOverMessage", ParametersJson = "{}", RunDate = Core.Helpers.AdjustTimeOffsetToUtc(DateTime.Today.AddHours(10).AddMinutes(30)) };
            var _jobLogic = ObjectFactory.GetInstance<IJobLogic>();
            _jobLogic.SaveOrUpdate(job);

            //add log
            var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
            var entity = new JobLog() { JobId = 0, Category = "System", Message = "Create voting is over mail job" };
            _jobLogLogic.SaveOrUpdate(entity);
        }

        private void CreateMorningMailJob()
        {
            var job = new Job() { CreatedDate = DateTime.Now, MethodName = "MorningMessage", ParametersJson = "{}", RunDate = Core.Helpers.AdjustTimeOffsetToUtc(DateTime.Today.AddHours(7).AddMinutes(30))};
            var _jobLogic = ObjectFactory.GetInstance<IJobLogic>();
            _jobLogic.SaveOrUpdate(job);

            //add log
            var _jobLogLogic = ObjectFactory.GetInstance<IJobLogLogic>();
            var entity = new JobLog() { JobId = 0, Category = "System", Message = "Create morning mail job" };
            _jobLogLogic.SaveOrUpdate(entity);
        }

        public void RunJob(string methodname, string parameters, int id)
        {
            var calledType = Type.GetType("Lunch.Core.Jobs.Jobs");
            if (calledType != null)
            {
                var methods = calledType.GetMethods(BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public);
                foreach (var method in methods)
                {
                    if (method.Name == methodname)
                    {
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
                var entity = new JobLog() { JobId = 0, Category = "Error" , Message=string.Format("Running job {0} failed",methodname) };
                _jobLogLogic.SaveOrUpdate(entity);
            }
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

        public static string GenerateRestaurantApi(string publisher, string latitude, string longitude, string radius, int page)
        {
            var apiUrl = ConfigurationManager.AppSettings.Get("RestaurantProviderURL");
            if (!string.IsNullOrWhiteSpace(apiUrl))
            {
                var sb = new StringBuilder();
                sb.Append(apiUrl);
                sb.Append(@"?type=restaurant&rpp=50&format=json");
                sb.Append(@"&publisher=" + publisher);
                sb.Append(@"&lat=" + latitude);
                sb.Append(@"&lon=" + longitude);
                sb.Append(@"&radius=" + radius);
                sb.Append(@"&page=" + page.ToString());
                apiUrl = sb.ToString();
            }
            return apiUrl;
        }
    }

}

