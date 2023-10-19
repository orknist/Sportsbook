namespace Sportsbook.Contracts.Models
{
    public record MatchModel(
        int Id,
        string Name,
        RoundModel Round,
        SportModel Sport,
        VenueModel Venue,
        string Status,
        CompetitionModel Competition,
        List<CompetitorModel> Competitors,
        DateTime StartTimeUtc);
}
