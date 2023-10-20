using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record RoundApiModel(int Id, string Name);

    public class RoundApiModelValidator : AbstractValidator<RoundApiModel>
    {
        public RoundApiModelValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
