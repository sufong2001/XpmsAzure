using ServiceStack.ServiceInterface;
using Xpms.Core.Models.Requests;
using Xpms.Core.Processes;

namespace Xpms.WebServices.Services
{
    public class PasswordResetService : Service
    {
        public PasswordResetProcess Process { set; get; }

        public object Any(PasswordResetRequest request)
        {
            object val = null;

            val = Process.PasswordResetRequest(request.Email);

            return val;
        }

        public object Any(PasswordResetVerificationRequest request)
        {
            object val = null;

            val = Process.PasswordResetVerification(request.Key);

            return val;
        }

        public object Any(PasswordResetConfirmRequest request)
        {
            object val = null;

            val = Process.PasswordReset(request);

            return val;
        }
    }
}