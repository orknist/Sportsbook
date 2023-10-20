using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Rounds")]
    public class RoundDapperEntity
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
