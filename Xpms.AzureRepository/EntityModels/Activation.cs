using Microsoft.WindowsAzure.Storage.Table;
using System;
using Xpms.AzureRepository.Constants;

namespace Xpms.AzureRepository.EntityModels
{
    public class Activation : TableEntity
    {
        public Activation()
        {
            this.PartitionKey = UsersTablePartition.UserActivation;
        }

        public string Email { get; set; }

        public string Salt { get; set; }

        public string SaltedHash { get; set; }

        public bool Sent { get; set; }

        public string UserRef { get; set; }
    }
}