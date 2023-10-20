using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record SportApiModel(int Id, string Name);

    public class SportApiModelValidator : AbstractValidator<SportApiModel>
    {
        public SportApiModelValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
