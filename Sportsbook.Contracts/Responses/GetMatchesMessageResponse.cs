using Sportsbook.Contracts.Models;

namespace Sportsbook.Contracts.Responses
{
    public class GetMatchesMessageResponse : BaseMessageResponse
    {
        public List<MatchMessageModel>? Matches { get; set; }
    }
}
