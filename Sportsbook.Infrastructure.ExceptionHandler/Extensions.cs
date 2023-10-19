using Microsoft.AspNetCore.Builder;

namespace Sportsbook.Infrastructure.ExceptionHandler
{
    public static class Extensions
    {
        // suggest a method name for following extension method (use IntelliSense)
        
        public static IApplicationBuilder UseExceptionHandlerConfiguration(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
    }
}
