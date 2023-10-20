namespace Sportsbook.Contracts.Models
{
    public record CompetitorMessageModel(int Id, string Name, string HomeAway, string CompetitorType, int? MatchId, MatchMessageModel? Match);
}
