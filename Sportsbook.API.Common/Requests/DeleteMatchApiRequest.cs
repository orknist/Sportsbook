using FluentValidation;

namespace Sportsbook.API.Common.Requests
{
    public record DeleteMatchApiRequest(int MatchId);

    public class DeleteMatchApiRequestValidator : AbstractValidator<DeleteMatchApiRequest>
    {
        public DeleteMatchApiRequestValidator()
        {
            RuleFor(x => x.MatchId).NotNull().GreaterThan(0);
        }
    }
}
