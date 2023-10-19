using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record CompetitionDTO(int Id, string Name);

    public class CompetitionDTOValidator : AbstractValidator<CompetitionDTO>
    {
        public CompetitionDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}