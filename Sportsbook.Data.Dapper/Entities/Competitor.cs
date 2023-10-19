using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Competitors")]
    public class Competitor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomeAway { get; set; }
        public string CompetitorType { get; set; }
        public int MatchId { get; set; }

        // Navigation property
        
        [Write(false)]
        public Match Match { get; set; }
    }
}
