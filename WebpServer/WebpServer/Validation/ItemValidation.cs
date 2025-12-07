using FluentValidation;
using WebpServer.Protocol;

namespace WebpServer.Validation
{
    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must be 50 characters or less.");
            RuleFor(x => x.Price)
                .InclusiveBetween(0, 1000000)
                .WithMessage("Price must be between 0 and 1,000,000.");
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be 0 or greater.");
        }
    }
}
