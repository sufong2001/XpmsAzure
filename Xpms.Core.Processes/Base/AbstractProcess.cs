using ServiceStack.Configuration;
using ServiceStack.Logging;
using Xpms.Core.IDB;
using Xpms.Core.ISecurity;
using Xpms.Core.Message;

namespace Xpms.Core.Processes.Base
{
    public abstract class AbstractProcess : IBusinessProcess
    {
        public IRepository Repository { get; set; }

        public IRepoUsers RepoUsers { get { return Repository.RepoUsers; } }

        public IRepoEvents RepoEvents { get { return Repository.RepoEvents; } }

        public IAuth Auth { get; set; }

        public IMailer Mailer { get; set; }

        public IContainerAdapter IoC { get; set; }

        public ILog Log { get { return LogManager.GetLogger(this.GetType()); } }
    }
}