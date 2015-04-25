using System;
using System.Net;
using System.Net.Mail;

namespace VotingSystem.WebUI.Models.Helpers
{
    public class SendEmail
    {
        public static bool SendEmailTo(string SentTo, string Text)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("votingsystemcorp@gmail.com");
            msg.To.Add(SentTo);
            msg.Subject = "Password";
            msg.Body = Text;
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);



            //client.UseDefaultCredentials = false;
            
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("votingsystemcorp@gmail.com", "Team62013");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}