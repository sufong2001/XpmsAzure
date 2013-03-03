using J.F.Libs.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.IDB.Data;
using Xpms.Core.Mails;
using Xpms.Core.Models.Requests;
using Xpms.Core.Processes.Base;

namespace Xpms.Core.Processes
{
    public class SignupProcess : AbstractProcess
    {
        public string Register(SignupRequest signup)
        {
            var user = RepoUsers.GetUserByUserNameOrEmail(signup.UserName, signup.Email);

            if (user != null) throw new UserAlreadyExistsException();

            string hash, salt;
            Auth.GetHashAndSaltString(signup.Password, out hash, out salt);

            user = IoC.Resolve<IUserRecord>();
            user.Email = signup.Email;
            user.UserName = signup.UserName;
            user.PasswordHash = hash;
            user.Salt = salt;

            var userId = RepoUsers.CreateUser(user);

            Auth.GetHashAndSaltString(userId, out hash, out salt);

            var key = RepoUsers.CreateActivation(userId, signup.Email, hash, salt);

            Mailer.Dispatch(new ActivationMail {ActivationKey = key, User = user});

            return key;
        }

        public string RegisterOpenAuth(SignupWithOpenidRequest signup)
        {
            var user = RepoUsers.GetUserByUserNameOrEmail(signup.UserName, signup.Email);

            if (user != null) throw new UserAlreadyExistsException();

            string hash, salt;
            Auth.GetHashAndSaltString(signup.Password, out hash, out salt);

            user = IoC.Resolve<IUserRecord>();
            user.OpenIds = signup.UserId.ToJsonArray();
            user.UserName = signup.UserName;
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