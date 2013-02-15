using ServiceStack.Configuration;
using Xpms.Core.IDB;
using Xpms.Core.ISecurity;

namespace Xpms.Core
{
    public interface IBusinessProcess
    {
        IAuth Auth { get; set; }

        IContainerAdapter IoC { get; set; }

        IRepository Repository { get; set; }

        IRepoUsers RepoUsers { get;  }
    }
}