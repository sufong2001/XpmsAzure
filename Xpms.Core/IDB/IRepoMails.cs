using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpms.Core.IDB.Data;

namespace Xpms.Core.IDB
{
    public interface IRepoMails
    {
        void Save(IMailRecord mail);
        T Get<T>() where T : IMailRecord;

    }
}
