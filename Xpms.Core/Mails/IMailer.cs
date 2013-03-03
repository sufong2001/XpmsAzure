using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xpms.Core.Mails
{
    public interface IMailer
    {
        void Dispatch<T>(T mail) where T : MailBase;
    }
}
