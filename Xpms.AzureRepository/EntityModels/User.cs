using Microsoft.WindowsAzure.Storage.Table;
using Xpms.AzureRepository.Constants;
using Xpms.Core.Constants;

namespace Xpms.AzureRepository.EntityModels
{
    public class User : TableEntity
    {
        public User()
        {
            this.PartitionKey = UsersTablePartition.User;
        }

        public string Id { 
            get { return this.RowKey; }
        }

        public string OpenIds { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public int Status { get; set; }

        public string Roles { get; set; }
    }
}