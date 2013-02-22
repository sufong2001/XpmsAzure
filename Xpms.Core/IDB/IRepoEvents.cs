using Xpms.Core.IDB.Data;

namespace Xpms.Core.IDB
{
    public interface IRepoEvents
    {
        string CreateEvent(IEventRecord eventData);
        void UpdateEvent(IEventRecord eventData);
        IEventRecord GetEvent(string eventId);
        void DeleteEvent(IEventRecord eventData);
    }
}