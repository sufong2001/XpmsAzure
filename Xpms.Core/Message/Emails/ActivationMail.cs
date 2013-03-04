using Xpms.Core.IDB.Data;

namespace Xpms.Core.Message.Emails
{
    public class ActivationMail : MailBase, IComposable<ActivationMail>
    {
        public string ActivationKey { get; set; }
        public IUserRecord User { get; set; }

        public ActivationMail Compose()
        {
            To = User.Email;
            Subject = "Account Activation";
            return this;
        }
    }
}
