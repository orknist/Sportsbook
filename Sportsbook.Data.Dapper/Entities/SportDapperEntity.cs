using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Sports")]
    public class SportDapperEntity
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
