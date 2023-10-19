using Sportsbook.Contracts.Models;

namespace Sportsbook.Contracts.Responses
{
    public class GetMatchesResponseModel : BaseResponseModel
    {
        public List<MatchModel>? Matches { get; set; }
    }
}
