using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sportsbook.API.Common
{
    public static class Extensions
    {
        public static IServiceCollection AddFluentValidationsConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
