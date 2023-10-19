using Sportsbook.API.Common.DTOs.Shared;

namespace Sportsbook.API.Common.DTOs
{
    public record GetMatchesDTO { }

    public class GetMatchesResultDTO : BaseResultDTO
    {
        public DateTime _CachedAt { get; set; } // For development test purpose only
        public List<MatchDTO>? Matches { get; set; }
    }
}
