using Xpms.Core.Constants;
using Xpms.Core.IDB.Data;
using Xpms.Core.Models;

namespace Xpms.Core.IDB
{
    public interface IRepoUsers
    {
        string CreateActivation(string userId, string email, string saltedHash, string salt);

        string CreatePasswordReset(string userId, string email, string hash, string salt);

        string CreateUser(string email, string passwordHash, string salt, UserStatus status = UserStatus.Signup);

        string CreateUser(IUserRecord userData);

        void DeleteActivationRecord(string userId);

        void DeletePasswordResetRecord(string userId);

        IActivationRecord GetActivationRecord(string hash);

        IPasswordResetRecord GetPasswordResetRecord(string hash);

        IUserRecord GetUserByEmail(string email);

        IUserRecord GetUserById(string id);

        void UpdatePasswordByEmail(string email, string passwordHash, string salt, UserStatus status = UserStatus.Activated);

        void UpdatePasswordByUserId(string userId, string passwordHash, string salt, UserStatus status = UserStatus.Activated);

        void UpdateUser(IUserRecord userData);

        void UpdateUserStatus(string userId, UserStatus status);
    }
}