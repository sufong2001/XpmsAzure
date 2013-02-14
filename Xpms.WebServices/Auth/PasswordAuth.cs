using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpms.Core.ISecurity;

namespace Xpms.WebServices.Auth
{
    public class PasswordAuth: IAuth
    {
        public void GetHashAndSaltString(string password, out string hash, out string salt)
        {
            new SaltedHash().GetHashAndSaltString(password, out hash, out salt);
        }

        public bool VerifyHashString(string password, string hash, string salt)
        {
            return new SaltedHash().VerifyHashString(password, hash, salt);
        }
    }
}
