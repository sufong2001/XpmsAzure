using Microsoft.WindowsAzure.Storage.Table;
using System;
using Xpms.AzureRepository.Constants;
using Xpms.Core.IDB.Data;

namespace Xpms.AzureRepository.EntityModels
{
    public class Event : TableEntity, IEventRecord
    {
        public Event()
        {
            this.PartitionKey = EventsTablePartition.Event;
        }

        public string Id
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string TimeZone { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Categories { get; set; }

        public string Guests { get; set; }
    }
}