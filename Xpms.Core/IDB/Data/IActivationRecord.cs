namespace Xpms.Core.IDB.Data
{
    public interface IActivationRecord : IRepoData
    {
        string Email { get; set; }
        string Salt { get; set; }
        string SaltedHash { get; set; }
        bool Sent { get; set; }
        string UserRef { get; set; }
    }
}