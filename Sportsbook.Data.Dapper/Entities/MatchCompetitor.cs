using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("MatchCompetitors")]
    public class MatchCompetitor
    {
        [ExplicitKey]
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int CompetitorId { get; set; }

        // Optional Data
        public string HomeAway { get; set; }
    }
}
