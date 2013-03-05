using System.Net.Mail;
using ServiceStack.Razor;
using ServiceStack.Razor.Templating;
using Xpms.Core.Exceptions;
using Xpms.Core.Message;

namespace Xpms.WebServices.Mail
{
    public static class RazorMailerExtensions
    {
        public static ViewPageRef GetMailTemplate(this RazorFormat razor, MailBase mail)
        {
            var name = mail.GetType().Name;
            return razor.GetViewPage(name);
        }

        public static T ComposeBody<T>(this T mail, RazorFormat format = null) where T : MailBase
        {
            format = format ?? RazorFormat.Instance;

            var viewRef = format.GetMailTemplate(mail);
            if (viewRef == null)
                throw new EmaiTemplateNotFoundException();

            mail.HtmlBody = format
                .ExecuteTemplate(mail, viewRef.Name, viewRef.Template)
                .Result;

            return mail;
        }
    }
}
