using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record VenueDTO(int Id, string Name);

    public class VenueDTOValidator : AbstractValidator<VenueDTO>
    {
        public VenueDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}