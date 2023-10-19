using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record HeaderDTO(DateTime TimeStampUtc);

    public class HeaderDTOValidator : AbstractValidator<HeaderDTO>
    {
        public HeaderDTOValidator()
        {
            RuleFor(x => x.TimeStampUtc).NotNull();
        }
    }
}
