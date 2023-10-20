using Microsoft.Extensions.DependencyInjection;
using Sportsbook.MatchConsumer.Business.Service;

namespace Sportsbook.MatchConsumer.Business
{
    public static class Extensions
    {
        public static IServiceCollection AddMatchConsumerConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping.MappingProfile).Assembly);
            services.AddScoped<IMatchConsumerService, DefaultMatchConsumerService>();

            return services;
        }
    }
}
