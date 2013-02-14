﻿using Xpms.Core.Constants;
using Xpms.Core.Models;

namespace Xpms.Core.IDB
{
    public interface IRepoUsers
    {
        string CreateActivation(string userId, string email, string saltedHash, string salt);

        string CreatePasswordReset(string userId, string email, string hash, string salt);

        string CreateUser(string email, string passwordHash, string salt, UserStatus status = UserStatus.Signup);

        string CreateUser(UserData userData);

        void DeleteActivationRecord(string userId);

        void DeletePasswordResetRecord(string userId);

        ActivationData GetActivationRecord(string hash);

        PasswordResetData GetPasswordResetRecord(string hash);

        UserData GetUserByEmail(string email);

        UserData GetUserById(string id);

        void UpdatePasswordByEmail(string email, string passwordHash, string salt, UserStatus status = UserStatus.Activated);

        void UpdatePasswordByUserId(string userId, string passwordHash, string salt, UserStatus status = UserStatus.Activated);

        void UpdateUser(UserData userData);

        void UpdateUserStatus(string userId, UserStatus status);
    }
}