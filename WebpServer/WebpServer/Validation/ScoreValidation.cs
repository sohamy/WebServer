
using FluentValidation;
using WebpServer.Protocol;

namespace WebpServer.Validation
{
    public class SubmitScoreRequestValidator : AbstractValidator<SubmitScoreRequest>
    {
        public SubmitScoreRequestValidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PlayerName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.ScoreValue)
                .GreaterThanOrEqualTo(0);
        }
    }

}
