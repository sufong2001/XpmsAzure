using Xpms.Core.IDB.Data;
using Xpms.Core.Models;
namespace Xpms.Core.IDB
{
    public interface IRepository
    {
        IRepoUsers RepoUsers { get; set; }

        IRepoEvents RepoEvents { get; set; }

        IRepoMails RepoMails { get; set; }
    }
}