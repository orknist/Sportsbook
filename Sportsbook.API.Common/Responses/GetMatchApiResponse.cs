using Sportsbook.API.Common.Models;

namespace Sportsbook.API.Common.Responses
{
    public class GetMatchApiResponse : BaseApiResponse
    {
        public DateTime _CachedAt { get; set; } // For development test purpose only
        public MatchApiModel? Match { get; set; }
    }
}
