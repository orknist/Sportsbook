using Sportsbook.Contracts.Models;

namespace Sportsbook.Contracts.Responses
{
    public class GetMatchMessageResponse : BaseMessageResponse
    {
        public MatchMessageModel? Match { get; set; }
    }
}
