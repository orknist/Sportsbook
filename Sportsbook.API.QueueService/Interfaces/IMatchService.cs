using Sportsbook.API.Common.DTOs;

namespace Sportsbook.API.QueueService.Interfaces
{
    public interface IMatchService
    {
        Task<AddMatchResultDTO> AddMatchAsync(AddMatchDTO dto);
        Task<GetMatchesResultDTO> GetMatchesAsync(GetMatchesDTO dto);
        Task<GetMatchResultDTO> GetMatchByIdAsync(GetMatchDTO dto);
        Task<DeleteMatchResultDTO> DeleteMatchByIdAsync(DeleteMatchDTO dto);
    }
}
