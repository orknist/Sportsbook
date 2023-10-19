using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sportsbook.Infrastructure.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConfig>(configuration.GetSection(nameof(RabbitMQConfig)));
            services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumers(Assembly.GetEntryAssembly());
                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    var config = configuration.GetSection(nameof(RabbitMQConfig)).Get<RabbitMQConfig>();
                    busFactoryConfigurator.Host(config.Host, config.VirtualHost, hostConfigurator =>
                    {
                        hostConfigurator.Username(config.Username);
                        hostConfigurator.Password(config.Password);
                    });
                    busFactoryConfigurator.ConfigureEndpoints(context);

                    var loggerFactory = context.GetRequiredService<ILoggerFactory>();
                    LogContext.ConfigureCurrentLogContext(loggerFactory);
                });
            });

            return services;
        }
    }
}
