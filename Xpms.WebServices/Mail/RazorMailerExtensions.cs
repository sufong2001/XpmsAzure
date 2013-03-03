using System.Net.Mail;
using ServiceStack.Razor;
using ServiceStack.Razor.Templating;
using Xpms.Core.Mails;

namespace Xpms.WebServices.Mail
{
    public static class RazorMailerExtensions
    {
        public static ViewPageRef GetMailTemplate<T>(this RazorFormat razor)
        {
            var name = typeof(T).Name;
            return razor.GetViewPage(name);
        }
    }
}
