using Microsoft.Extensions.DependencyInjection;
using Sportsbook.Data.Dapper.Repositories;
using Sportsbook.Data.Repositories;

namespace Sportsbook.Data.Dapper
{
    public static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddAutoMapper(typeof(Mapping.MappingProfile).Assembly);

            return services;
        }
    }
}
