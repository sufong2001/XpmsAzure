using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Xpms.AzureRepository.Constants;
using Xpms.AzureRepository.DAO;
using Xpms.Core.IDB;

namespace Xpms.AzureRepository
{
    public class AzureStorage : IRepository
    {

        internal string StorageConnectionString { get; set; }

        internal CloudTable TableUsers { get; set; }
        internal CloudTable TableEvents { get; set; }
        internal CloudQueue QueueMailout { get; set; }

        public IRepoUsers RepoUsers { get; set; }
        public IRepoEvents RepoEvents { get; set; }
        public IRepoMails RepoMails { get; set; }

        private AzureStorage(string connectionString)
        {
            StorageConnectionString = connectionString;

            CreateStorageIfNotExists();

            RepoUsers = new DaoUsers(this);
            RepoMails = new DaoMails(this);
        }

        private static AzureStorage SingleAzureStorage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString")</param>
        /// <returns></returns>
        public static AzureStorage CreateSingleton(string connectionString)
        {
            if (SingleAzureStorage == null)
            {
                SingleAzureStorage = new AzureStorage(connectionString);
            }

            return SingleAzureStorage;
        }

        private void CreateStorageIfNotExists()
        {
            var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // If this is running in a Windows Azure Web Site (not a Cloud Service) use the Web.config file:
            //    var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();
            
            TableUsers = tableClient.GetTableReference(XpmsTable.Users);
            TableUsers.CreateIfNotExists();

            TableEvents = tableClient.GetTableReference(XpmsTable.Events);
            TableEvents.CreateIfNotExists();

            var queueClient = storageAccount.CreateCloudQueueClient();
            QueueMailout = queueClient.GetQueueReference(XpmsQueue.Mailouts);
            QueueMailout.CreateIfNotExists();

            //var blobClient = storageAccount.CreateCloudBlobClient();
            //var blobContainer = blobClient.GetContainerReference("azuremailblobcontainer");
            //blobContainer.CreateIfNotExists();
        }
    }
}