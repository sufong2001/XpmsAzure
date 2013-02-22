using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceInterface;
using Xpms.Core.Models.Requests;
using Xpms.Core.Processes;
using Xpms.WebServices.Extensions;

namespace Xpms.WebServices
{
    public class Debug : Service
    {
        public SignupProcess Process { set; get; }

        public object Any(DebugRequest request)
        {

            return this.GetSession();
        }
    }
}
