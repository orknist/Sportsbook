using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Matches")]
    public class MatchDapperEntity
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
        public RoundDapperEntity Round { get; set; }

        [Write(false)]
        public SportDapperEntity Sport { get; set; }

        [Write(false)]
        public VenueDapperEntity Venue { get; set; }
        
        [Write(false)]
        public CompetitionDapperEntity Competition { get; set; }

        [Write(false)]
        public List<CompetitorDapperEntity> Competitors { get; set; }
    }
}
