using ServiceStack.ServiceInterface;
using Xpms.Core.Constants;
using Xpms.Core.Models;
using Xpms.Core.Processes;
using Xpms.WebServices.Extensions;

namespace Xpms.WebServices
{
    public class SignupService : Service
    {
        public SignupProcess Process { set; get; }

        public object Get(SignupRequest request)
        {
            switch (request.Stage)
            {
                case SignupStage.Request:
                    Process.Register(request);
                    break;

                case SignupStage.OpenAuthRequest:
                    request = this.GetSession()
                        .GetOpenAuthSignupRequest();
                    Process.RegisterOpenAuth(request);
                    break;

                case SignupStage.Activation:
                    Process.Activation(request.ActivationKey);
                    break;
            }
            return this.GetSession();
        }

        public object Post(SignupRequest request)
        {
            Process.Register(request);

            return "ok";
        }

        public object Put(SignupRequest request)
        {
            return null;
        }

        public object Patch(SignupRequest request)
        {
            string val = null;
            switch (request.Stage)
            {
                case SignupStage.Request:
                    val = Process.Register(request);
                    break;
            }

            return val;
        }

        public void Delete(SignupRequest request)
        {
        }
    }
}