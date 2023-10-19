using Sportsbook.Contracts.Models;

namespace Sportsbook.Contracts.Responses
{
    public class GetMatchResponseModel : BaseResponseModel
    {
        public MatchModel? Match { get; set; }
    }
}
