using System;

namespace Xpms.Core.IDB.Data
{
    public interface IEventRecord
    {
        string Id { get; set; }

        string Title { get; set; }

        DateTime StartDateTime { get; set; }

        DateTime? EndDateTime { get; set; }

        string TimeZone { get; set; }

        string Description { get; set; }

        string Location { get; set; }

        string Categories { get; set; }

        string Guests { get; set; }
    }
}