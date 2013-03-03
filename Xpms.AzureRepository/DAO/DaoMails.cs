using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using Xpms.AzureRepository.Constants;
using Xpms.AzureRepository.EntityModels;
using Xpms.AzureRepository.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.IDB;
using Xpms.Core.IDB.Data;
using Xpms.Core.Models;

namespace Xpms.AzureRepository.DAO
{
    public class DaoMails : IRepoMails
    {
        public AzureStorage Storage { get; set; }

        public CloudQueue MailoutQueue { get { return Storage.QueueMailout; } }

        public DaoMails(AzureStorage storage)
        {
            Storage = storage;
        }

        public void Save(IMailRecord mail)
        {
            
        }
    }
}