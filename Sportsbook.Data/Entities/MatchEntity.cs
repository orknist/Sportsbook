namespace Sportsbook.Data.Entities
{
    public class MatchEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoundId { get; set; }
        public int SportId { get; set; }
        public int VenueId { get; set; }
        public string Status { get; set; }
        public int CompetitionId { get; set; }
        public DateTime StartTimeUtc { get; set; }

        // Navigation properties
        public RoundEntity Round { get; set; }
        public SportEntity Sport { get; set; }
        public VenueEntity Venue { get; set; }
        public CompetitionEntity Competition { get; set; }
        public List<CompetitorEntity> Competitors { get; set; }
    }
}
