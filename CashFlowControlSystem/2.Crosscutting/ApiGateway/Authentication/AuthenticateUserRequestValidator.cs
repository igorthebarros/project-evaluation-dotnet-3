using FluentValidation;

namespace ApiGateway.Authentication
{
    /// <summary>
    /// Validator for AuthenticateUserRequest
    /// </summary>
    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        /// <summary>
        /// Initializes validation rules for AuthenticateUserRequest
        /// </summary>
        public AuthenticateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }

}
