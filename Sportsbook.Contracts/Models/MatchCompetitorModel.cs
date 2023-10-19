namespace Sportsbook.Contracts.Models
{
    public record MatchCompetitorModel(int Id, int MatchId, int CompetitorId, string HomeAway);
}
