using Xpms.Core.Constants;

namespace Xpms.Core.Models
{
    public class ForgotPasswordRequest
    {
        public PasswordResetStage Stage { get; set; }

        public string Email { get; set; }

        public string Key { get; set; }

        public string Hash { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}