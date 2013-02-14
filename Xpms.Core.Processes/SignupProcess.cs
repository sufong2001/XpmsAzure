using J.F.Libs.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.Models;
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

            var key =  RepoUsers.CreateActivation(userId, signup.Email, hash, salt);

            return key;
        }

        public string RegisterOpenAuth(SignupRequest signup)
        {
            var user = RepoUsers.GetUserByEmail(signup.Email);

            if (user != null) throw new UserEmailAlreadyExistsException();

            string hash, salt;
            Auth.GetHashAndSaltString(signup.Password, out hash, out salt);

            var userId = RepoUsers.CreateUser(new UserData
                {
                    OpenIds = signup.UserId.ToJsonArray(),
                    Email = signup.Email,
                    DisplayName = signup.DisplayName,
                    FirstName = signup.FirstName,
                    LastName = signup.LastName,
                    PasswordHash = hash,
                    Salt = salt,
                    Status = (int) UserStatus.Activated,
                    Roles = UserRoles.Register.ToJsonArray()
                });

            return userId;
        }

        public bool Activation(string activationKey)
        {
            var record = RepoUsers.GetActivationRecord(activationKey);

            if (record == null) throw new InvalidActivationException();

            var user = RepoUsers.GetUserById(record.UserRef);
            user.Status = (int) UserStatus.Activated;
            user.Roles = UserRoles.Register.ToJsonArray();

            RepoUsers.UpdateUser(user);

            RepoUsers.DeleteActivationRecord(record.UserRef);

            return true;
        }
    }
}