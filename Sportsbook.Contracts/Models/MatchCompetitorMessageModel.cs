namespace Sportsbook.Contracts.Models
{
    public record MatchCompetitorMessageModel(int Id, int MatchId, int CompetitorId, string HomeAway);
}
