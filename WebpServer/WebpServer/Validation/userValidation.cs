using FluentValidation;
using WebpServer.Protocol;

namespace WebpServer.Validation
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is invalid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");

            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Nickname is required.")
                .MaximumLength(20).WithMessage("Nickname must be 20 characters or less.");

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120)
                .WithMessage("Age must be between 1 and 120.");
        }
    }
}
