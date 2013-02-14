using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Xpms.AzureRepository.Constants;
using Xpms.AzureRepository.DAO;
using Xpms.Core.IDB;

namespace Xpms.AzureRepository
{
    public class AzureStorage : IRepository
    {
        internal CloudTable TableUsers { get; set; }

        public IRepoUsers RepoUsers { get; set; }

        private AzureStorage()
        {
            CreateStorageIfNotExists();

            RepoUsers = new DaoUsers(this);
        }

        private static AzureStorage SingleAzureStorage { get; set; }

        public static AzureStorage CreateSingleton()
        {
            if (SingleAzureStorage == null)
            {
                SingleAzureStorage = new AzureStorage();
            }

            return SingleAzureStorage;
        }

        private void CreateStorageIfNotExists()
        {
            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            // If this is running in a Windows Azure Web Site (not a Cloud Service) use the Web.config file:
            //    var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();
            TableUsers = tableClient.GetTableReference(XpmsTable.Users);
            TableUsers.CreateIfNotExists();

            //var blobClient = storageAccount.CreateCloudBlobClient();
            //var blobContainer = blobClient.GetContainerReference("azuremailblobcontainer");
            //blobContainer.CreateIfNotExists();

            //var queueClient = storageAccount.CreateCloudQueueClient();
            //var subscribeQueue = queueClient.GetQueueReference("azuremailsubscribequeue");
            //subscribeQueue.CreateIfNotExists();
        }
    }
}