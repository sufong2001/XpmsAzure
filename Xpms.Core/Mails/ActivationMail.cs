using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpms.Core.IDB.Data;

namespace Xpms.Core.Mails
{
    public class ActivationMail : MailBase
    {
        public string ActivationKey { get; set; }
        public IUserRecord User { get; set; }

        public override void Compose()
        {
            To = User.Email;
            Subject = "Account Activation";
        }
    }
}
