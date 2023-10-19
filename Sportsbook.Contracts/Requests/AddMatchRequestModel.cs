using Sportsbook.Contracts.Models;

namespace Sportsbook.Contracts.Requests
{
    public record AddMatchRequestModel(MatchModel Match, HeaderModel Header);
}
