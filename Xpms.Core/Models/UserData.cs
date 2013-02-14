using Xpms.Core.IDB;
using J.F.Libs.Extensions;

namespace Xpms.Core.Models
{
    public class UserData : IRepoData
    {
        public string Id { get; set; }

        public string OpenIds { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public int Status { get; set; }

        public string Roles { get; set; }

        public bool HasOpenId(string openId)
        {
            if (OpenIds == null) return false;

            return OpenIds.JsonArrayConatins(openId);
        }
    }
}