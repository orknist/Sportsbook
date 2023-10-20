namespace Sportsbook.Data.Entities
{
    public class MatchCompetitor
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int CompetitorId { get; set; }

        // Optional Data
        public string HomeAway { get; set; }
    }
}
