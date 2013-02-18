using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xpms.Core.Constants
{
    public enum UserStatus
    {
        Signup,
        Activated,
        Deactivated
    }

    [Flags]
    public enum UserRoles
    {
        Register,
        User,
        AccountUser,
        AccountManager,
        AccountSubscriber,
        Admin,
        Root
    }
}
