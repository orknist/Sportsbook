using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record CompetitorApiModel(int Id, string Name, string HomeAway, string CompetitorType);

    public class CompetitorApiModelValidator : AbstractValidator<CompetitorApiModel>
    {
        public CompetitorApiModelValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.HomeAway).NotNull().NotEmpty();
            RuleFor(x => x.CompetitorType).NotNull().NotEmpty();
        }
    }
}
