using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using ServiceStack.Text;
using Xpms.AzureRepository.Constants;
using Xpms.AzureRepository.EntityModels;
using Xpms.AzureRepository.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.IDB;
using Xpms.Core.IDB.Data;
using ServiceStack.Text.Jsv;

namespace Xpms.AzureRepository.DAO
{


    public class DaoMails : IRepoMails
    {
        public AzureStorage Storage { get; set; }

        public CloudQueue MailoutQueue
        {
            get { return Storage.QueueMailout; }
        }

        public DaoMails(AzureStorage storage)
        {
            Storage = storage;
        }

        public void Save(IMailRecord mail)
        {
            string msg = TypeSerializer.SerializeToString(mail);

            MailoutQueue.AddMessage(new CloudQueueMessage(msg));
        }

        public T Get<T>() where T : IMailRecord
        {
            var msg = MailoutQueue.GetMessage();
            MailoutQueue.DeleteMessage(msg);

            return TypeSerializer.DeserializeFromString<T>(msg.AsString);
        }
    }
}