using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Competitions")]
    public class CompetitionDapperEntity
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
