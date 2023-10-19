using FluentValidation;
using Sportsbook.API.Common.DTOs.Shared;

namespace Sportsbook.API.Common.DTOs
{
    public record AddMatchDTO(MatchDTO Match, HeaderDTO Header);

    public class AddMatchDTOValidator : AbstractValidator<AddMatchDTO>
    {
        public AddMatchDTOValidator()
        {
            RuleFor(x => x.Match).NotNull().SetValidator(new MatchDTOValidator());
            RuleFor(x => x.Header).NotNull().SetValidator(new HeaderDTOValidator());
        }
    }

    public class AddMatchResultDTO : BaseResultDTO
    {
        public int? MatchId { get; set; }
    }
}
