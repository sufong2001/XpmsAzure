namespace Xpms.Core.Message
{
    public interface IMailer
    {
        void Dispatch<T>(T mail) where T : MailBase, IComposable<T>;
    }
}