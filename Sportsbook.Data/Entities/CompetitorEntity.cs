namespace Sportsbook.Data.Entities
{
    public class CompetitorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomeAway { get; set; }
        public string CompetitorType { get; set; }
        public int MatchId { get; set; }

        // Navigation property
        public MatchEntity Match { get; set; }
    }
}
