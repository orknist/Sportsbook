using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record RoundDTO(int Id, string Name);

    public class RoundDTOValidator : AbstractValidator<RoundDTO>
    {
        public RoundDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}