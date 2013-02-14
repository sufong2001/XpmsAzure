using ServiceStack.ServiceInterface;
using Xpms.Core.Constants;
using Xpms.Core.Models;
using Xpms.Core.Processes;

namespace Xpms.WebServices
{
    public class PasswordReserService : Service
    {
        public PasswordResetProcess Process { set; get; }


        public object Any(ForgotPasswordRequest request)
        {
            object val = null;

            switch (request.Stage)
            {
                case PasswordResetStage.Request:
                    val = Process.PasswordResetRequest(request.Email);
                    break;

                case PasswordResetStage.Verification:
                    val = Process.PasswordResetVerification(request.Key);
                    break;

                case PasswordResetStage.Reset:
                    val = Process.PasswordReset(request);
                    break;
            }

            return val;
        }

        //public object Post(ForgotPassword request)
        //{
        //    return null;
        //}

        //public object Put(Signup request)
        //{
        //    return null;
        //}

        //public void Delete(Signup request)
        //{
        //}
    }
}