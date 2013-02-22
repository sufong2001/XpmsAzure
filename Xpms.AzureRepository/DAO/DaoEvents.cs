using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using Xpms.AzureRepository.Constants;
using Xpms.AzureRepository.EntityModels;
using Xpms.AzureRepository.Extensions;
using Xpms.Core.IDB;
using Xpms.Core.IDB.Data;

namespace Xpms.AzureRepository.DAO
{
    public class DaoEvents : IRepoEvents
    {
        public AzureStorage Storage { get; set; }

        public CloudTable EventsTable { get { return Storage.TableEvents; } }

        public DaoEvents(AzureStorage storage)
        {
            Storage = storage;
        }

        public string CreateEvent(IEventRecord eventData)
        {
            var reventRecord = eventData as Event;
            if (reventRecord == null) throw new ArgumentException("Invalid Event Type.");

            reventRecord.Id = Guid.NewGuid().ToString();

            EventsTable.Insert(reventRecord);

            return reventRecord.Id;
        }

        public void UpdateEvent(IEventRecord eventData)
        {
            var reventRecord = eventData as Event;
            if (reventRecord == null) throw new ArgumentException("Invalid Event Type.");

            EventsTable.Update(reventRecord);
        }

        public IEventRecord GetEvent(string eventId)
        {
            var query = EventsTable.AsQueryable<Event>()
                         .Where(u => u.PartitionKey == EventsTablePartition.Event
                             && u.RowKey == eventId)
                         .ToArray().FirstOrDefault();

            return query;
        }

        public void DeleteEvent(IEventRecord eventData)
        {
            var reventRecord = eventData as Event;
            if (reventRecord == null) throw new ArgumentException("Invalid Event Type.");

            EventsTable.Delete(reventRecord);
        }
    }
}