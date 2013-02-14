using Xpms.Core.IDB;
using Xpms.Core.ISecurity;

namespace Xpms.Core.Processes.Base
{
    public abstract class AbstractProcess : IBusinessProcess
    {
        public IRepository Repository { get; set; }

        public IRepoUsers RepoUsers { get { return Repository.RepoUsers; } }

        public IAuth Auth { get; set; }

    }
}
