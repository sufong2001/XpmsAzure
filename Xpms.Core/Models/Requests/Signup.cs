namespace Xpms.Core.Models.Requests
{
    public class SignupRequest : IRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class SignupWithOpenidRequest : SignupRequest
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }
    }

    public class SignupActivationRequest : IRequest
    {
        public string ActivationKey { get; set; }
    }
}