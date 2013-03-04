using J.F.Libs.Extensions;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Xpms.Core.Message
{
    public class Mailman : SmtpClient
    {
        public Mailman()
        {
            Host = ConfigurationManager.AppSettings["mailer.host"] ?? "127.0.0.1";
            Port = ConfigurationManager.AppSettings["mailer.port"].ConvertTo(25);
            Credentials = new NetworkCredential
            {
                UserName = ConfigurationManager.AppSettings["mailer.username"],
                Password = ConfigurationManager.AppSettings["mailer.password"]
            };
        }

        public void Send(MailBase mail)
        {
            if (mail.From.IsNullOrEmpty())
                mail.From = ConfigurationManager.AppSettings["mailer.from"];

            if (mail.To.IsNullOrEmpty())
                mail.To = ConfigurationManager.AppSettings["mailer.to"] ?? "admin@to.no.where";

            if (mail.Subject.IsNullOrEmpty())
                mail.Subject = "[NO SUBJECT]";

            Send(mail.Message());
        }
    }
}