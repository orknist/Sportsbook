using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record HeaderApiModel(DateTime TimeStampUtc);

    public class HeaderApiModelValidator : AbstractValidator<HeaderApiModel>
    {
        public HeaderApiModelValidator()
        {
            RuleFor(x => x.TimeStampUtc).NotNull();
        }
    }
}
