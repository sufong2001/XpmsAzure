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
    public class DaoUsers : IRepoUsers
    {
        public AzureStorage Storage { get; set; }

        public CloudTable UsersTable { get { return Storage.TableUsers; } }

        public DaoUsers(AzureStorage storage)
        {
            Storage = storage;
        }

        public string CreateUser(string email, string passwordHash, string salt, UserStatus status = UserStatus.Signup)
        {
            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Salt = salt,
                Status = (int) status
            };

            return CreateUser(user);
        }

        public string CreateUser(IUserRecord userData)
        {
            var user = userData as User;
            if(user == null) throw new ArgumentException("Invalid User Type.");

            user.RowKey = Guid.NewGuid().ToString();

            UsersTable.Insert(user);

            return user.Id;
        }

        private User GetUser(string userId = null, string userName = null, string email = null)
        {
            var query = from u in UsersTable.AsQueryable<User>()
                        where u.PartitionKey == UsersTablePartition.User
                        select u;

            if (userName != null && email != null)
                query = query.Where(u => u.UserName == userName || u.Email == email);
            else if (userName != null)
                query = query.Where(u => u.UserName == userName);
            else if (email != null)
                query = query.Where(u => u.Email == email);
            else
                query = query.Where(u => u.RowKey == userId);


            return query.FirstOrDefault();
        }

        public IUserRecord GetUserById(string id)
        {
            return GetUser(id);
        }

        public IUserRecord GetUserByUserName(string userName)
        {
            return GetUser(userName: userName);
        }

        public IUserRecord GetUserByUserNameOrEmail(string userName, string email)
        {
            return GetUser(userName: userName, email: email);
        }

        public IUserRecord GetUserByEmail(string email)
        {
            return GetUser(email: email);
        }

        public void UpdateUser(IUserRecord userData)
        {
            var user = userData as User;
            if (user == null) throw new ArgumentException("Invalid User Type.");

            UsersTable.Update(user);
        }

        public void UpdateUserStatus(string userId, UserStatus status)
        {
            var user = GetUser(userId: userId);

            user.Status = (int) status;

            UsersTable.Update(user);
        }

        public string CreateActivation(string userId, string email, string saltedHash, string salt)
        {
            var activation = new Activation
            {
                RowKey = Guid.NewGuid().ToString(),
                UserRef = userId,
                Email = email,
                SaltedHash = saltedHash,
                Salt = salt
            };

            UsersTable.Insert(activation);

            return activation.RowKey;
        }

        public IActivationRecord GetActivationRecord(string key)
        {
            var query = from r in UsersTable.AsQueryable<Activation>()
                         where r.PartitionKey == UsersTablePartition.UserActivation
                               && r.RowKey == key
                         select r;

            IActivationRecord record = query.FirstOrDefault()
                .MapTo<IActivationRecord>();

            return record;
        }

        public void DeleteActivationRecord(string userId)
        {
            var records = (from u in UsersTable.AsQueryable<Activation>()
                           where u.PartitionKey == UsersTablePartition.UserActivation
                               && u.UserRef == userId
                           select u).ToArray();

            if (records.Length > 0)
                UsersTable.DeleteBatch(records);
        }

        public string CreatePasswordReset(string userId, string email, string hash, string salt)
        {
            var reset = new PasswordReset()
            {
                RowKey = Guid.NewGuid().ToString(),
                UserRef = userId,
                Email = email,
                SaltedHash = hash,
                Salt = salt
            };

            UsersTable.Insert(reset);

            return reset.RowKey;
        }

        public IPasswordResetRecord GetPasswordResetRecord(string key)
        {
            var query = from r in UsersTable.AsQueryable<PasswordReset>()
                         where r.PartitionKey == UsersTablePartition.UserPasswordReset
                               && r.RowKey == key
                               && r.ExpiryDate > DateTime.UtcNow
                         select r;

            IPasswordResetRecord record = query.FirstOrDefault()
                .MapTo<IPasswordResetRecord>();

            return record;
        }

        public void DeletePasswordResetRecord(string userId)
        {
            var records = (from u in UsersTable.AsQueryable<PasswordReset>()
                           where u.PartitionKey == UsersTablePartition.UserPasswordReset
                               && u.UserRef == userId
                           select u).ToArray();

            if (records.Length > 0)
                UsersTable.DeleteBatch(records);
        }

        public void UpdatePasswordByEmail(string email, string passwordHash, string salt, UserStatus status = UserStatus.Activated)
        {
            UpdatePassword(passwordHash, salt, email: email);
        }

        public void UpdatePasswordByUserId(string userId, string passwordHash, string salt, UserStatus status = UserStatus.Activated)
        {
            UpdatePassword(passwordHash, salt, userId: userId);
        }

        private void UpdatePassword(string passwordHash, string salt, string userId = null, string email = null, UserStatus status = UserStatus.Activated)
        {
            var user = userId != null ? GetUser(userId: userId) : GetUser(email: email);
            if (user == null) throw new InvalidUserException();

            user.PasswordHash = passwordHash;
            user.Salt = salt;
            user.Status = (int) status;

            UsersTable.Update(user);
        }
    }
}