namespace Xpms.Core.IDB.Data
{
    public interface IUserRecord : IRepoData
    {
        string Id { get; set; }

        string OpenIds { get; set; }

        string DisplayName { get; set; }

        string UserName { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string PasswordHash { get; set; }

        string Salt { get; set; }

        int Status { get; set; }

        string Roles { get; set; }

        bool HasOpenId(string openId);
    }
}