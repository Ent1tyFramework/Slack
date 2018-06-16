using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Slack.Identity.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var from = ConfigurationManager.AppSettings["Email"];
            var pass = ConfigurationManager.AppSettings["Password"];

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}
