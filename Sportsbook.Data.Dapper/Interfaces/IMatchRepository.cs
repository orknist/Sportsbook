using Sportsbook.Data.Dapper.Entities;
using System.Data;

namespace Sportsbook.Data.Dapper.Interfaces
{
    public interface IMatchRepository
    {
        Task<int> AddMatchAsync(Match match);
        Task<Match?> GetMatchByIdAsync(int id);
        Task<List<Match>> GetAllMatchesAsync();
        Task<bool> UpdateMatchAsync(Match match);
        Task<bool> DeleteMatchAsync(int id);
        IDbConnection GetDbConnection();
    }
}
