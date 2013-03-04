using System.Threading;
using Xpms.Core.IDB;
using Xpms.Core.Message;

namespace Xpms.WebServices.Mail
{
    public class Mailer : IMailer
    {
        public IRepository Storage { get; set; }

        public IRepoMails MailQueue { get { return Storage.RepoMails; } }

        public void Dispatch<T>(T mail) where T : MailBase, IComposable<T>
        {
            var draft = mail
                .Compose()
                .ComposeBody()
                .ToDraft();

            MailQueue.Save(draft);

            ThreadPool.QueueUserWorkItem(obj =>
                {
                    var draftMail = MailQueue.Get<MailDraft>();
                    var email = draftMail.LoadDraft();
                    email.Send();
                });
        }
    }
}