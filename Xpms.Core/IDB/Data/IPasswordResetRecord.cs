namespace Xpms.Core.IDB.Data
{
    public interface IPasswordResetRecord : IRepoData
    {
        string UserRef { get; set; }
        string Email { get; set; }
        string SaltedHash { get; set; }
        string Salt { get; set; }
    }
}