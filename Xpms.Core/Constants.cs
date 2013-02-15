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

    public enum SignupStage
    {
        Request,
        OpenAuthRequest,
        Activation,
        Cancellation
    }

    public enum PasswordResetStage
    {
        Request,
        Verification,
        Reset
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
