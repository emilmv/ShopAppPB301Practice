using System.Net;
using System.Net.Mail;
using ShopAppPB301Practice.Services.Interfaces;

namespace ShopAppPB301Practice.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public void SendEmail(List<string> emails, string subject, string body)
        {
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("example@example.com", "Shop App");
            foreach (var adress in emails)
            {
                mailMessage.To.Add(adress);
            }
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            SmtpClient smtpClient = new();
            smtpClient.Host = "yourAddress";
            smtpClient.Port = 0;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("youremailaddress@example.com", "your password");
            smtpClient.Send(mailMessage);
        }
    }
}
