using ServiceStack.FluentValidation;
using Xpms.Core.Models.Requests;

namespace Xpms.WebServices.Validators
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
        }
    }

    public class SignupActivationRequestValidator : AbstractValidator<SignupActivationRequest>
    {
        public SignupActivationRequestValidator()
        {
            RuleFor(x => x.ActivationKey).NotEmpty();
        }
    }
}