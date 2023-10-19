using Dapper;
using Dapper.Contrib.Extensions;
using Sportsbook.Data.Dapper.Entities;
using Sportsbook.Data.Dapper.Interfaces;
using System.Data;

namespace Sportsbook.Data.Dapper.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IDbConnection _dbConnection;

        public MatchRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddMatchAsync(Match match)
        {
            if (await _dbConnection.GetAsync<Competition>(match.CompetitionId) == null)
                await _dbConnection.InsertAsync(match.Competition);

            if (await _dbConnection.GetAsync<Round>(match.RoundId) == null)
                await _dbConnection.InsertAsync(match.Round);

            if (await _dbConnection.GetAsync<Sport>(match.SportId) == null)
                await _dbConnection.InsertAsync(match.Sport);

            if (await _dbConnection.GetAsync<Venue>(match.VenueId) == null)
                await _dbConnection.InsertAsync(match.Venue);

            var matchId = await _dbConnection.InsertAsync(match);
            if (match.Competitors != null && match.Competitors.Any())
            {
                foreach (var competitor in match.Competitors)
                {
                    competitor.Match = match;
                    competitor.MatchId = matchId;
                    await _dbConnection.InsertAsync(competitor);
                }
            }

            return matchId;
        }

        public async Task<Match?> GetMatchByIdAsync(int id)
        {
            var sql = @"
                SELECT m.*, r.*, s.*, v.*, c.*
                FROM Matches m
                LEFT JOIN Rounds r ON m.RoundId = r.Id
                LEFT JOIN Sports s ON m.SportId = s.Id
                LEFT JOIN Venues v ON m.VenueId = v.Id
                LEFT JOIN Competitions c ON m.CompetitionId = c.Id
                WHERE m.Id = @MatchId
                LIMIT 1
            ";

            var match = (await _dbConnection.QueryAsync<Match, Round, Sport, Venue, Competition, Match>(sql, (match, round, sport, venue, competition) =>
            {
                match.Round = round;
                match.Sport = sport;
                match.Venue = venue;
                match.Competition = competition;
                return match;
            }, new { MatchId = id }, splitOn: "Id,Id,Id,Id")).FirstOrDefault();

            if (match != null)
                match.Competitors = (await _dbConnection.QueryAsync<Competitor>("SELECT * FROM Competitors WHERE MatchId=@MatchId", new { MatchId = match.Id })).ToList();

            return match;
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            var sql = @"
                SELECT m.*, r.*, s.*, v.*, c.*
                FROM Matches m
                LEFT JOIN Rounds r ON m.RoundId = r.Id
                LEFT JOIN Sports s ON m.SportId = s.Id
                LEFT JOIN Venues v ON m.VenueId = v.Id
                LEFT JOIN Competitions c ON m.CompetitionId = c.Id
            ";

            var matches = (await _dbConnection.QueryAsync<Match, Round, Sport, Venue, Competition, Match>(sql, (match, round, sport, venue, competition) =>
            {
                match.Round = round;
                match.Sport = sport;
                match.Venue = venue;
                match.Competition = competition;
                return match;
            },
            splitOn: "Id,Id,Id,Id")).ToList();

            matches.ForEach(async match =>
            {
                match.Competitors = (await _dbConnection.QueryAsync<Competitor>("SELECT * FROM Competitors WHERE MatchId=@MatchId", new { MatchId = match.Id })).ToList();
            });

            return matches;
        }

        public async Task<bool> UpdateMatchAsync(Match match)
        {
            return await _dbConnection.UpdateAsync(match);
        }

        public async Task<bool> DeleteMatchAsync(int id)
        {
            var match = await GetMatchByIdAsync(id);
            if (match != null)
            {
                return await _dbConnection.DeleteAsync(match);
            }
            return false;
        }

        public IDbConnection GetDbConnection()
        {
            return _dbConnection;
        }
    }
}
