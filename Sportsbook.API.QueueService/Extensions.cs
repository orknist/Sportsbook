using Microsoft.Extensions.DependencyInjection;
using Sportsbook.API.QueueService.Interfaces;
using Sportsbook.API.QueueService.Services;

namespace Sportsbook.API.QueueService
{
    public static class Extensions
    {
        public static IServiceCollection AddQueueServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMatchService, MatchService>();
            return services;
        }
    }
}
