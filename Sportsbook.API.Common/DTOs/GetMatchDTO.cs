using FluentValidation;
using Sportsbook.API.Common.DTOs.Shared;

namespace Sportsbook.API.Common.DTOs
{
    public record GetMatchDTO(int MatchId);

    public class GetMatchDTOValidator : AbstractValidator<GetMatchDTO>
    {
        public GetMatchDTOValidator()
        {
            RuleFor(x => x.MatchId).NotNull().GreaterThan(0);
        }
    }

    public class GetMatchResultDTO : BaseResultDTO
    {
        public DateTime _CachedAt { get; set; } // For development test purpose only
        public MatchDTO? Match { get; set; }
    }
}
