using FluentValidation;

namespace Sportsbook.API.Common.Requests
{
    public record GetMatchApiRequest(int MatchId);

    public class GetMatchApiRequestValidator : AbstractValidator<GetMatchApiRequest>
    {
        public GetMatchApiRequestValidator()
        {
            RuleFor(x => x.MatchId).NotNull().GreaterThan(0);
        }
    }
}
