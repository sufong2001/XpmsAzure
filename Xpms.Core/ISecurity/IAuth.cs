using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xpms.Core.ISecurity
{
    public interface IAuth
    {
        void GetHashAndSaltString(string password, out string hash, out string salt);
        bool VerifyHashString(string password, string hash, string salt);
    }
}
