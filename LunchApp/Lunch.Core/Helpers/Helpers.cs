using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Core.Helpers
{
    class Helpers
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




    }
}
