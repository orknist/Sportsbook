namespace Sportsbook.Contracts.Models
{
    public record MatchMessageModel(
        int Id,
        string Name,
        RoundMessageModel Round,
        SportMessageModel Sport,
        VenueMessageModel Venue,
        string Status,
        CompetitionMessageModel Competition,
        List<CompetitorMessageModel> Competitors,
        DateTime StartTimeUtc);
}
