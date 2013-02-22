using Xpms.Core.Models;
namespace Xpms.Core.IDB
{
    public interface IRepository
    {
        IRepoUsers RepoUsers { get; set; }

        IRepoEvents RepoEvents { get; set; }
    }
}