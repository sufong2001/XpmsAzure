using Xpms.Core.IDB;
using Xpms.Core.ISecurity;

namespace Xpms.Core.Processes.Base
{
    public interface IBusinessProcess
    {
        IAuth Auth { get; set; }

        IRepository Repository { get; set; }

        IRepoUsers RepoUsers { get;  }
    }
}