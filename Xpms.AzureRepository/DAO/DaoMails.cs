using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using ServiceStack.Text;
using Xpms.AzureRepository.Constants;
using Xpms.AzureRepository.Extensions;
using Xpms.Core.IDB;
using Xpms.Core.Message;

namespace Xpms.AzureRepository.DAO
{
    public class DaoMails : IRepoMails
    {
        public AzureStorage Storage { get; set; }

        public CloudQueue MailoutQueue { get { return Storage.QueueMailout; } }

        public CloudBlobContainer MailoutBlobContainer { get { return Storage.BlobContainerMailAttachments; } }

        public DaoMails(AzureStorage storage)
        {
            Storage = storage;
        }

        public void Save(MailBase mail)
        {
            var draft = mail.ToDraft();

            var nameref = draft.GenerateHtmlBodyRef();
            MailoutBlobContainer
                .GetBlockBlobReference(nameref)
                .Upload(mail.HtmlBody);

            string msg = TypeSerializer.SerializeToString(draft);

            MailoutQueue.AddMessage(new CloudQueueMessage(msg));
        }

        public T Get<T>() where T : MailBase
        {
            var msg = MailoutQueue.GetMessage();
            MailoutQueue.DeleteMessage(msg);

            var draft = TypeSerializer.DeserializeFromString<MailDraft>(msg.AsString);

            var mail = draft.LoadBlobContent(MailoutBlobContainer);

            return (T)mail;
        }
    }

    internal static class DaoMailExtensions
    {
        public static string FormatBlobName(this MailDraft draft, string name)
        {
            return string.Format("{0}/{1}/{2}", XpmsBlobContainer.MailoutAttachments, draft.Id, name);
        }

        public static string GenerateHtmlBodyRef(this MailDraft draft)
        {
            draft.HtmlBodyRef = draft.FormatBlobName("htmlbodyref");
            return draft.HtmlBodyRef;
        }

        public static string GenerateTextBodyRef(this MailDraft draft)
        {
            draft.TextBodyRef = draft.FormatBlobName("textbodyref");
            return draft.TextBodyRef;
        }

        public static MailBase LoadBlobContent(this MailDraft draft, CloudBlobContainer blobContainer, bool deleteAfterLoaded = true)
        {
            var mail = draft.LoadDraft();

            var blob = blobContainer
                .GetBlockBlobReference(draft.HtmlBodyRef);

            mail.HtmlBody = blob.GetBlobText();

            if (deleteAfterLoaded) blob.Delete();

            return mail;
        }
    }
}