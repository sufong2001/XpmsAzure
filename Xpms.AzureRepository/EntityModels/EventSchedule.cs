using Microsoft.WindowsAzure.Storage.Table;
using System;
using Xpms.AzureRepository.Constants;
using Xpms.Core.IDB.Data;

namespace Xpms.AzureRepository.EntityModels
{
    public class EventSchedule : TableEntity
    {
        public EventSchedule()
        {
            this.PartitionKey = EventsTablePartition.EventScheduleDefault;
        }

        public string Id
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public DateTime DateTime { get; set; }
        public string EventRef { get; set; }
    }
}