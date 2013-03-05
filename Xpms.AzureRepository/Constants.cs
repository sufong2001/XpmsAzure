using System;

namespace Xpms.AzureRepository.Constants
{
    public class UsersTablePartition
    {
        public static string User = "user";
        public static string UserActivation = "useractivation";
        public static string UserContact = "usercontact";
        public static string UserPasswordReset = "userpasswordreset";
    }

    public class EventsTablePartition
    {
        private static readonly string ScheduleDateFormat = "yyyy-MM-dd";

        public static string Event = "event";

        public static string EventScheduleDefault { get { return DateTime.UtcNow.ToString(ScheduleDateFormat); } }

        public static string EventSchedule(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString(ScheduleDateFormat);
        }
    }

    public class XpmsTable
    {
        public static string Users = "users";
        public static string Events = "events";
    }

    public class XpmsQueue
    {
        public static string Mailouts = "mailouts";
    }

    public class XpmsBlobContainer
    {
        public static string MailoutAttachments = "mailoutattachments";
    }
}