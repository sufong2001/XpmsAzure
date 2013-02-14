using Xpms.Core.IDB;
namespace Xpms.Core.Models
{
    public class ActivationData : IRepoData
    {
        public string UserRef { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }
    }
}