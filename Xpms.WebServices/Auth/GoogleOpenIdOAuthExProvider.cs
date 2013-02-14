using System.Collections.Generic;
using System.Globalization;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using ServiceStack.Authentication.OpenId;
using ServiceStack.Configuration;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using Xpms.Core.IDB;
using J.F.Libs.Extensions;
using Xpms.WebServices.Extensions;

namespace Xpms.WebServices.Auth
{
    public class GoogleOpenIdOAuthExProvider : OpenIdOAuthProvider
    {
        public IRepository Repository { get; set; }

        public const string Name = "GoogleOpenId";
        public static string Realm = "https://www.google.com/accounts/o8/id";

        public GoogleOpenIdOAuthExProvider(IResourceManager appSettings)
            : base(appSettings, Name, Realm) { }

        protected override void LoadUserAuthInfo(AuthUserSession userSession, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            base.LoadUserAuthInfo(userSession, tokens, authInfo);
            Repository.LoadUserAuth(userSession, tokens);
        }

        protected override Dictionary<string, string> CreateAuthInfo(IAuthenticationResponse response)
        {
            // This is where you would look for any OpenID extension responses included
            // in the authentication assertion.
            var claimsResponse = response.GetExtension<ClaimsResponse>();
            
            claimsResponse.PatchCulture(); // Patch the Culture Object conversion issue
            
            var authInfo = claimsResponse.ToDictionary();

            authInfo["user_id"] = response.ClaimedIdentifier; //a url

            // Store off the "friendly" username to display -- NOT for username lookup
            authInfo["openid_ref"] = response.FriendlyIdentifierForDisplay;

            var provided = GetAttributeEx(response);
            foreach (var entry in provided)
            {
                authInfo[entry.Key] = entry.Value;
            }

            return authInfo;
        }

        /// <summary>
        /// Extracts an Attribute Exchange response, if one exists
        /// </summary>
        private Dictionary<string, string> GetAttributeEx(IAuthenticationResponse response)
        {
            var ret = new Dictionary<string, string>();

            var fetchResponse = response.GetExtension<FetchResponse>();

            if (fetchResponse == null) return ret;

            var names = new List<string>();
            var emails = new List<string>();

            if (fetchResponse.Attributes.Contains("http://schema.openid.net/namePerson"))
                names.AddRange(fetchResponse.Attributes["http://schema.openid.net/namePerson"].Values);

            if (fetchResponse.Attributes.Contains(WellKnownAttributes.Name.FullName))
                names.AddRange(fetchResponse.Attributes[WellKnownAttributes.Name.FullName].Values);

            if (fetchResponse.Attributes.Contains(WellKnownAttributes.Name.Alias))
                names.AddRange(fetchResponse.Attributes[WellKnownAttributes.Name.Alias].Values);

            if (fetchResponse.Attributes.Contains(WellKnownAttributes.Name.First))
                names.AddRange(fetchResponse.Attributes[WellKnownAttributes.Name.First].Values);

            if (fetchResponse.Attributes.Contains(WellKnownAttributes.Name.Last))
                names.AddRange(fetchResponse.Attributes[WellKnownAttributes.Name.Last].Values);

            if (fetchResponse.Attributes.Contains("http://schema.openid.net/contact/email"))
                emails.AddRange(fetchResponse.Attributes["http://schema.openid.net/contact/email"].Values);

            if (fetchResponse.Attributes.Contains(WellKnownAttributes.Contact.Email))
                emails.AddRange(fetchResponse.Attributes[WellKnownAttributes.Contact.Email].Values);

            if (names.Count > 0)
                ret["FullName"] = names[0];

            if (emails.Count > 0)
                ret["Email"] = emails[0];

            return ret;
        }
    }

    public static class OpenIdExtensionsEx
    {
        internal static void PatchCulture(this ClaimsResponse response)
        {
            if (!response.Language.IsNullOrEmpty() && response.Language.Contains("-"))
            {
                var language = response.Language;
                var country = response.Country;

                response.Culture = CultureInfo.GetCultureInfo(language);
                response.Language = language;
                response.Country = country;
            }
        }
    }
}