using Xpms.Core.Constants;

namespace Xpms.Core.Models
{
    public class SignupRequest
    {
        public SignupStage Stage { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ActivationKey { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }
    }
}