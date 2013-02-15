using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J.F.Libs.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.IDB;
using Xpms.Core.Models;
using ServiceStack.ServiceInterface.Auth;
using Xpms.Core.Models.Requests;

namespace Xpms.WebServices.Extensions
{
    public static class AuthSessionExtensions
    {
        public static SignupWithOpenIdRequest GetOpenAuthSignupRequest(this IAuthSession session)
        {
            var token = session.ProviderOAuthAccess.LastOrDefault();

            if (token == null) throw new ArgumentException("Invalid Open Auth Signup Request.");

            var pass = session.CreatedAt.ToString();

            return new SignupWithOpenIdRequest
                {
                   UserId =  token.UserId,
                   Email = session.Email,
                   Password = pass,
                   ConfirmPassword = pass,
                   DisplayName = session.DisplayName,
                   FirstName = session.FirstName,
                   LastName = session.LastName
                };
        }

        public static void LoadUserAuth(this IRepository repository, AuthUserSession userSession, IOAuthTokens tokens)
        {
            userSession.DisplayName = tokens.DisplayName;
            userSession.FullName = tokens.FullName;
            userSession.Email = tokens.Email;
            userSession.UserName = tokens.Email;

            var user = repository.RepoUsers.GetUserByEmail(tokens.Email);
            if (user == null || !user.HasOpenId(tokens.UserId)) return;

            userSession.Roles = user.Roles.FromJsonString<List<string>>();
        }
    }
}
