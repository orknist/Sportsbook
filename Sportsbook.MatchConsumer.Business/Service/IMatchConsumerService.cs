using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;

namespace Sportsbook.MatchConsumer.Business.Service
{
    public interface IMatchConsumerService
    {
        Task<AddMatchMessageResponse> AddMatchAsync(AddMatchMessageRequest requestModel);
        Task<DeleteMatchMessageResponse> DeleteMatchAsync(DeleteMatchMessageRequest requestModel);
        Task<GetMatchMessageResponse> GetMatchAsync(GetMatchMessageRequest requestModel);
        Task<GetMatchesMessageResponse> GetMatchesAsync(GetMatchesMessageRequest requestModel);
    }
}
