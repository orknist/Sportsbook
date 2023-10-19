using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record CompetitorDTO(int Id, string Name, string HomeAway, string CompetitorType);

    public class CompetitorDTOValidator : AbstractValidator<CompetitorDTO>
    {
        public CompetitorDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.HomeAway).NotNull().NotEmpty();
            RuleFor(x => x.CompetitorType).NotNull().NotEmpty();
        }
    }
}
