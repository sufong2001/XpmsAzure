using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.Models;
using Xpms.Core.Processes.Base;

namespace Xpms.Core.Processes
{
    public class PasswordResetProcess : AbstractProcess
    {

        public string PasswordResetRequest(string email)
        {
            var user = RepoUsers.GetUserByEmail(email);

            if (email == null) throw new InvalidUserResetException();

            RepoUsers.DeletePasswordResetRecord(user.Id);

            string hash, salt;
            Auth.GetHashAndSaltString(user.Id, out hash, out salt);

            var key = RepoUsers.CreatePasswordReset(user.Id, user.Email, hash, salt);

            return key;
        }

        public string PasswordResetVerification(string key)
        {
            var record = RepoUsers.GetPasswordResetRecord(key);

            if (record == null)
                throw new InvalidPasswordResetException();

            return record.SaltedHash;
        }

        public string PasswordReset(ForgotPasswordRequest request)
        {
            var record = RepoUsers.GetPasswordResetRecord(request.Key);

            if (record == null || record.SaltedHash != request.Hash)
                throw new InvalidPasswordResetException();

            string hash, salt;
            Auth.GetHashAndSaltString(request.NewPassword, out hash, out salt);

            RepoUsers.UpdatePasswordByUserId(record.UserRef, hash, salt);

            // clearup
            RepoUsers.DeletePasswordResetRecord(record.UserRef);

            // if people forgot to activation their signup, 
            // password reset can also act as signup activation
            RepoUsers.DeleteActivationRecord(record.UserRef);


            return request.Key;
        }
    }
}
