﻿using Dapper.Contrib.Extensions;

namespace Sportsbook.Data.Dapper.Entities
{
    [Table("Venues")]
    public class Venue
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
