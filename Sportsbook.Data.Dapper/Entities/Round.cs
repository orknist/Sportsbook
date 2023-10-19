using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Rounds")]
    public class Round
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
