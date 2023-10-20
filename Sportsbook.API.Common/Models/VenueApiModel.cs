using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record VenueApiModel(int Id, string Name);

    public class VenueApiModelValidator : AbstractValidator<VenueApiModel>
    {
        public VenueApiModelValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
