using Sportsbook.API.Common.Requests;
using Sportsbook.API.Common.Responses;

namespace Sportsbook.API.QueueService.Interfaces
{
    public interface IMatchService
    {
        Task<AddMatchApiResponse> AddMatchAsync(AddMatchApiRequest request);
        Task<GetMatchesApiResponse> GetMatchesAsync(GetMatchesApiRequest request);
        Task<GetMatchApiResponse> GetMatchByIdAsync(GetMatchApiRequest request);
        Task<DeleteMatchApiResponse> DeleteMatchByIdAsync(DeleteMatchApiRequest request);
    }
}
