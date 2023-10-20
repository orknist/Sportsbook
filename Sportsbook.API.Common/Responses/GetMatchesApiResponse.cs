using Sportsbook.API.Common.Models;

namespace Sportsbook.API.Common.Responses
{
    public class GetMatchesApiResponse: BaseApiResponse
    {
        public DateTime _CachedAt { get; set; } // For development test purpose only
        public List<MatchApiModel>? Matches { get; set; }
    }
}
