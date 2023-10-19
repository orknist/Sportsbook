using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Matches")]
    public class Match
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoundId { get; set; }
        public int SportId { get; set; }
        public int VenueId { get; set; }
        public string Status { get; set; }
        public int CompetitionId { get; set; }
        public DateTime StartTimeUtc { get; set; }

        // Navigation properties

        [Write(false)]
        public Round Round { get; set; }

        [Write(false)]
        public Sport Sport { get; set; }

        [Write(false)]
        public Venue Venue { get; set; }
        
        [Write(false)]
        public Competition Competition { get; set; }

        [Write(false)]
        public List<Competitor> Competitors { get; set; }
    }
}
