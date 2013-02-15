namespace Xpms.Core.Models.Requests
{
    public class PasswordResetRequest : IRequest
    {
        public string Email { get; set; }
    }

    public class PasswordResetVerificationRequest : IRequest
    {
        public string Key { get; set; }
    }

    public class PasswordResetConfirmRequest : PasswordResetVerificationRequest
    {
        public string Hash { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}