using Sportsbook.Data.Entities;
using System.Data;

namespace Sportsbook.Data.Repositories
{
    public interface IMatchRepository
    {
        Task<int> AddMatchAsync(MatchEntity match);
        Task<MatchEntity?> GetMatchByIdAsync(int id);
        Task<List<MatchEntity>> GetAllMatchesAsync();
        Task<bool> UpdateMatchAsync(MatchEntity match);
        Task<bool> DeleteMatchAsync(int id);
        IDbConnection GetDbConnection();
    }
}
