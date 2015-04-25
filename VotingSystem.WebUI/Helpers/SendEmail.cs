namespace VotingSystem.WebUI.Helpers
{
	using System;
	using System.Net;
	using System.Net.Mail;

	public class SendEmail
	{
		public static bool SendEmailTo(string sendTo, string text)
		{
			var msg = new MailMessage {From = new MailAddress("votingsystemcorp@gmail.com")};

			msg.To.Add(sendTo);
			msg.Subject = "Password";
			msg.Body = text;
			msg.Priority = MailPriority.High;
			msg.IsBodyHtml = true;

			var client = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential("votingsystemcorp@gmail.com", "Team62013"),
				DeliveryMethod = SmtpDeliveryMethod.Network
			};

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