using J.F.Libs.Extensions;
using Xpms.Core.Constants;
using Xpms.Core.Exceptions;
using Xpms.Core.IDB.Data;
using Xpms.Core.Models.Requests;
using Xpms.Core.Processes.Base;

namespace Xpms.Core.Processes
{
    public class EventProcess : AbstractProcess
    {
        public string Create(EventRequest request)
        {
            var eventRecord = IoC.Resolve<IEventRecord>();


            var key = RepoEvents.CreateEvent(eventRecord);

            return key;
        }
    }
}