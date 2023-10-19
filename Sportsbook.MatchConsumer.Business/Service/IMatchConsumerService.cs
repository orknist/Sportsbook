using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;

namespace Sportsbook.MatchConsumer.Business.Service
{
    public interface IMatchConsumerService
    {
        Task<AddMatchResponseModel> AddMatchAsync(AddMatchRequestModel requestModel);
        Task<DeleteMatchResponseModel> DeleteMatchAsync(DeleteMatchRequestModel requestModel);
        Task<GetMatchResponseModel> GetMatchAsync(GetMatchRequestModel requestModel);
        Task<GetMatchesResponseModel> GetMatchesAsync(GetMatchesRequestModel requestModel);
    }
}
