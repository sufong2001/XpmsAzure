using ServiceStack.ServiceInterface;
using Xpms.Core.Models.Requests;
using Xpms.Core.Processes;
using Xpms.WebServices.Extensions;

namespace Xpms.WebServices
{
    public class SignupService : Service
    {
        public SignupProcess Process { set; get; }

        public object Any(SignupRequest request)
        {
            var key = Process.Register(request);
            return key;
        }

        public object Any(SignupWithOpenIdRequest request)
        {
            request = this.GetSession()
                          .GetOpenAuthSignupRequest();

            Process.RegisterOpenAuth(request);

            return this.GetSession();
        }

        public object Any(SignupActivationRequest request)
        {
            Process.Activation(request.ActivationKey);
            return this.GetSession();
        }
    }
}