using Xpms.Core.IDB;
namespace Xpms.Core.Models
{
    public class PasswordResetData : IRepoData
    {
        public string UserRef { get; set; }

        public string Email { get; set; }

        public string SaltedHash { get; set; }

        public string Salt { get; set; }
    }
}