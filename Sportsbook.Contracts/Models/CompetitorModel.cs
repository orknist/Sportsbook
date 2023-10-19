namespace Sportsbook.Contracts.Models
{
    public record CompetitorModel(int Id, string Name, string HomeAway, string CompetitorType, int? MatchId, MatchModel? Match);
}
