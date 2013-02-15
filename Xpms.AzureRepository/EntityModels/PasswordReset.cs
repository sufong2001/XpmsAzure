using Microsoft.WindowsAzure.Storage.Table;
using System;
using Xpms.AzureRepository.Constants;
using Xpms.Core.IDB.Data;

namespace Xpms.AzureRepository.EntityModels
{
    public class PasswordReset : TableEntity, IPasswordResetRecord
    {
        public PasswordReset()
        {
            this.PartitionKey = UsersTablePartition.UserPasswordReset;
            ExpiryDate = DateTime.UtcNow.AddHours(1);
        }

        public string Email { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string Salt { get; set; }

        public string SaltedHash { get; set; }

        public bool Sent { get; set; }

        public string UserRef { get; set; }
    }
}