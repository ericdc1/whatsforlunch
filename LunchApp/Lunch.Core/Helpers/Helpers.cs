using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lunch.Core.Logic.Implementations;
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
                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(toAddress));

                // From
                mailMsg.From = new MailAddress(fromAddress);

                // Subject and multipart/alternative Body
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_15781ccf35dcb3784a8bbde4c537d708@azure.com", "rb8t8vev");
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

        public void CreateJobs(DateTime dateTime)
        {
            CreateMorningMailJob();
            CreateVotingIsOverJob();
            CreateWhereAreWeGoingJob();
        }

        private void CreateWhereAreWeGoingJob()
        {
            //TODO:insert into db this job
            throw new NotImplementedException();
        }

        private void CreateVotingIsOverJob()
        {
            //TODO: insert into db this job
            throw new NotImplementedException();
        }

        private void CreateMorningMailJob()
        {
            //TODO:insert into db this job
            throw new NotImplementedException();
        }

        public void RunJob(string methodname, string parameters)
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
                        calledType.InvokeMember(methodname, BindingFlags.InvokeMethod| BindingFlags.Public | BindingFlags.Instance, null, jobsInstance, new object[] { parameters });
                        break;
                    }
                }
            }
            else
            {
                //ToDo - email or log that this failed
            }
        }
    }

}

