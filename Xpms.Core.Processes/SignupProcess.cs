﻿using J.F.Libs.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.IDB.Data;
using Xpms.Core.Models.Requests;
using Xpms.Core.Processes.Base;

namespace Xpms.Core.Processes
{
    public class SignupProcess : AbstractProcess
    {
        public string Register(SignupRequest signup)
        {
            var user = RepoUsers.GetUserByEmail(signup.Email);

            if (user != null) throw new UserEmailAlreadyExistsException();

            string hash, salt;
            Auth.GetHashAndSaltString(signup.Password, out hash, out salt);

            var userId = RepoUsers.CreateUser(signup.Email, hash, salt);

            Auth.GetHashAndSaltString(userId, out hash, out salt);

            var key = RepoUsers.CreateActivation(userId, signup.Email, hash, salt);

            return key;
        }

        public string RegisterOpenAuth(SignupWithOpenIdRequest signup)
        {
            var user = RepoUsers.GetUserByEmail(signup.Email);

            if (user != null) throw new UserEmailAlreadyExistsException();

            string hash, salt;
            Auth.GetHashAndSaltString(signup.Password, out hash, out salt);

            user = IoC.Resolve<IUserRecord>();
            user.OpenIds = signup.UserId.ToJsonArray();
            user.Email = signup.Email;
            user.DisplayName = signup.DisplayName;
            user.FirstName = signup.FirstName;
            user.LastName = signup.LastName;
            user.PasswordHash = hash;
            user.Salt = salt;
            user.Status = (int)UserStatus.Activated;
            user.Roles = UserRoles.Register.ToJsonArray();

            var userId = RepoUsers.CreateUser(user);

            return userId;
        }

        public bool Activation(string activationKey)
        {
            var record = RepoUsers.GetActivationRecord(activationKey);

            if (record == null) throw new InvalidActivationException();

            var user = RepoUsers.GetUserById(record.UserRef);
            user.Status = (int)UserStatus.Activated;
            user.Roles = UserRoles.Register.ToJsonArray();

            RepoUsers.UpdateUser(user);

            RepoUsers.DeleteActivationRecord(record.UserRef);

            return true;
        }
    }
}