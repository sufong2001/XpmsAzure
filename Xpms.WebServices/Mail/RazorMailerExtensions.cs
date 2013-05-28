using ServiceStack.Razor;
using ServiceStack.Razor.Managers;
using Xpms.Core.Exceptions;
using Xpms.Core.Message;

namespace Xpms.WebServices.Mail
{
    public static class RazorMailerExtensions
    {
        public static RazorPage GetMailTemplate(this RazorFormat razor, MailBase mail)
        {
            var name = mail.GetType().Name;
            return razor.GetPageByName(name);
        }

        public static T ComposeBody<T>(this T mail, RazorFormat format = null) where T : MailBase
        {
            format = format ?? RazorFormat.Instance;

            var page = format.GetMailTemplate(mail);
            if (page == null)
                throw new EmaiTemplateNotFoundException();

            mail.HtmlBody = format.RenderToHtml(page, mail);

            return mail;
        }
    }
}