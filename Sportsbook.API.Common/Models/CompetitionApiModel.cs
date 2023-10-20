using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record CompetitionApiModel(int Id, string Name);

    public class CompetitionApiModelValidator : AbstractValidator<CompetitionApiModel>
    {
        public CompetitionApiModelValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}