namespace Sportsbook.Infrastructure.Redis
{
    public class RedisConfig
    {
        public string? ConnectionString { get; set; }
        public TimeSpan? AbsoluteExpiration { get; set; }
    }
}
