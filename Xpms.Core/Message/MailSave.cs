using System;
using Xpms.Core.IDB.Data;

namespace Xpms.Core.Message
{
    public class MailDraft : IMailRecord
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string MailType { get; set; }

        public string HtmlBodyRef { get; set; }

        public string TextBodyRef { get; set; }
    }
}