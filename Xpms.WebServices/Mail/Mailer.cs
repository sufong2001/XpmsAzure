using ServiceStack.Razor;
using Xpms.Core.Exceptions;
using Xpms.Core.IDB;
using Xpms.Core.Mails;
using Xpms.Core.Processes.MailProcess;

namespace Xpms.WebServices.Mail
{
    public class Mailer : IMailer
    {
        public IRepository Storage { get; set; }

        public void Dispatch<T>(T mail) where T : MailBase
        {
            Compose(mail);

            mail.Send();
        }

        public void Compose<T>(T mail) where T : MailBase
        {
            var format = RazorFormat.Instance;

            var viewRef = format.GetMailTemplate<T>();
            if (viewRef == null)
                throw new EmaiTemplateNotFoundException();

            mail.HtmlBody = format
                .ExecuteTemplate(mail, viewRef.Name, viewRef.Template)
                .Result;

            mail.Compose();
        }
    }
}