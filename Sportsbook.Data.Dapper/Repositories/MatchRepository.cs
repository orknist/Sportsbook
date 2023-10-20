using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Sportsbook.Data.Dapper.Entities;
using Sportsbook.Data.Entities;
using Sportsbook.Data.Repositories;
using System.Data;

namespace Sportsbook.Data.Dapper.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public MatchRepository(IMapper mapper, IDbConnection dbConnection)
        {
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public async Task<int> AddMatchAsync(MatchEntity entity)
        {
            var dapperEntity = _mapper.Map<MatchDapperEntity>(entity);

            if (await _dbConnection.GetAsync<CompetitionDapperEntity>(dapperEntity.CompetitionId) == null)
                await _dbConnection.InsertAsync(dapperEntity.Competition);

            if (await _dbConnection.GetAsync<RoundDapperEntity>(dapperEntity.RoundId) == null)
                await _dbConnection.InsertAsync(dapperEntity.Round);

            if (await _dbConnection.GetAsync<SportDapperEntity>(dapperEntity.SportId) == null)
                await _dbConnection.InsertAsync(dapperEntity.Sport);

            if (await _dbConnection.GetAsync<VenueDapperEntity>(dapperEntity.VenueId) == null)
                await _dbConnection.InsertAsync(dapperEntity.Venue);

            var matchId = await _dbConnection.InsertAsync(dapperEntity);
            if (dapperEntity.Competitors != null && dapperEntity.Competitors.Any())
            {
                foreach (var competitor in dapperEntity.Competitors)
                {
                    competitor.Match = dapperEntity;
                    competitor.MatchId = matchId;
                    await _dbConnection.InsertAsync(competitor);
                }
            }

            return matchId;
        }

        public async Task<MatchEntity?> GetMatchByIdAsync(int id)
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

            var matchDapperEntity = (await _dbConnection.QueryAsync<MatchDapperEntity, RoundDapperEntity, SportDapperEntity, VenueDapperEntity, CompetitionDapperEntity, MatchDapperEntity>(sql, (match, round, sport, venue, competition) =>
            {
                match.Round = round;
                match.Sport = sport;
                match.Venue = venue;
                match.Competition = competition;
                return match;
            }, new { MatchId = id }, splitOn: "Id,Id,Id,Id")).FirstOrDefault();

            if (matchDapperEntity != null)
                matchDapperEntity.Competitors = (await _dbConnection.QueryAsync<CompetitorDapperEntity>("SELECT * FROM Competitors WHERE MatchId=@MatchId", new { MatchId = matchDapperEntity.Id })).ToList();

            var matchEntity = _mapper.Map<MatchEntity>(matchDapperEntity);
            return matchEntity;
        }

        public async Task<List<MatchEntity>> GetAllMatchesAsync()
        {
            var sql = @"
                SELECT m.*, r.*, s.*, v.*, c.*
                FROM Matches m
                LEFT JOIN Rounds r ON m.RoundId = r.Id
                LEFT JOIN Sports s ON m.SportId = s.Id
                LEFT JOIN Venues v ON m.VenueId = v.Id
                LEFT JOIN Competitions c ON m.CompetitionId = c.Id
            ";

            var matchDapperEntities = (await _dbConnection.QueryAsync<MatchDapperEntity, RoundDapperEntity, SportDapperEntity, VenueDapperEntity, CompetitionDapperEntity, MatchDapperEntity>(sql, (match, round, sport, venue, competition) =>
            {
                match.Round = round;
                match.Sport = sport;
                match.Venue = venue;
                match.Competition = competition;
                return match;
            },
            splitOn: "Id,Id,Id,Id")).ToList();

            matchDapperEntities.ForEach(async match =>
            {
                match.Competitors = (await _dbConnection.QueryAsync<CompetitorDapperEntity>("SELECT * FROM Competitors WHERE MatchId=@MatchId", new { MatchId = match.Id })).ToList();
            });

            var matchEntities = _mapper.Map<List<MatchEntity>>(matchDapperEntities);
            return matchEntities;
        }

        public Task<bool> UpdateMatchAsync(MatchEntity match)
        {
            throw new NotImplementedException();
            //return await _dbConnection.UpdateAsync(match);
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
