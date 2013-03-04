namespace Xpms.Core.Message.Emails
{
    public class WelcomeMail : MailBase, IComposable<WelcomeMail>
    {
        public WelcomeMail Compose()
        {
            return this;
        }
    }
}
