using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record SportDTO(int Id, string Name);

    public class SportDTOValidator : AbstractValidator<SportDTO>
    {
        public SportDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}