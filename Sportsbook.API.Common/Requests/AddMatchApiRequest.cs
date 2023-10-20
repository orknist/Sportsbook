using FluentValidation;
using Sportsbook.API.Common.Models;

namespace Sportsbook.API.Common.Requests
{
    public record AddMatchApiRequest(MatchApiModel Match, HeaderApiModel Header);

    public class AddMatchApiRequestValidator : AbstractValidator<AddMatchApiRequest>
    {
        public AddMatchApiRequestValidator()
        {
            RuleFor(x => x.Match).NotNull().SetValidator(new MatchApiModelValidator());
            RuleFor(x => x.Header).NotNull().SetValidator(new HeaderApiModelValidator());
        }
    }
}
