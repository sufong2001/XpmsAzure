using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J.F.Libs.Extensions;
using ServiceStack.Authentication.OpenId;
using ServiceStack.ServiceInterface.Auth;
using Xpms.Core.IDB;
using ServiceStack.Configuration;
using Xpms.WebServices.Extensions;

namespace Xpms.WebServices.Auth
{
    public class YahooOpenIdOAuthExProvider : YahooOpenIdOAuthProvider
    {
        public IRepository Repository { get; set; }

        public YahooOpenIdOAuthExProvider(IResourceManager appSettings)
            : base(appSettings) { }

        protected override void LoadUserAuthInfo(AuthUserSession userSession, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            base.LoadUserAuthInfo(userSession, tokens, authInfo);
            Repository.LoadUserAuth(userSession, tokens);
        }
    }
}
