using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpms.Core.IDB.Data;
using Xpms.Core.Message;

namespace Xpms.Core.IDB
{
    public interface IRepoMails
    {
        void Save(MailBase mail);
        T Get<T>() where T : MailBase;

    }
}
