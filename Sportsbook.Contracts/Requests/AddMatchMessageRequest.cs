using Sportsbook.Contracts.Models;

namespace Sportsbook.Contracts.Requests
{
    public record AddMatchMessageRequest(MatchMessageModel Match, HeaderMessageModel Header);
}
