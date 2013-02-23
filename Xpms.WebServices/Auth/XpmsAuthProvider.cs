using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using Xpms.Core.IDB;
using Xpms.Core.ISecurity;

namespace Xpms.WebServices.Auth
{
    public class XpmsAuthProvider : CredentialsAuthProvider
    {
        public IRepository Repository { get; set; }
        public IAuth Auth { get; set; }

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
        {
            var user = Repository.RepoUsers.GetUserByUserNameOrEmail(userName, userName);

            if (user == null) return false;
            //Add here your custom auth logic (database calls etc)
            //Return true if credentials are valid, otherwise false
            return Auth.VerifyHashString(password, user.PasswordHash, user.Salt);
        }

        protected override void LoadUserAuthInfo(AuthUserSession userSession, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            var user = Repository.RepoUsers.GetUserByUserNameOrEmail(userSession.UserAuthName, userSession.UserAuthName);
            userSession.Email = user.Email;
            userSession.UserName = user.UserName;

            base.LoadUserAuthInfo(userSession, tokens, authInfo);
        }
    }
}
