using System;

namespace Xpms.Core.Models.Requests
{
    public class EventRequest : IRequest
    {
        public string Id { get; set; }

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