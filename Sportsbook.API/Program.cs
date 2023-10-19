using Sportsbook.API.QueueService;
using Sportsbook.Data.Redis;
using Sportsbook.Infrastructure.ExceptionHandler;
using Sportsbook.Infrastructure.MassTransit;
using Sportsbook.Infrastructure.Swagger;
using System.Text.Json.Serialization;
using Sportsbook.API.Common;

// Create Builder
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Add Services
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration(); // Extension method from Sportsbook.Infrastructure.Swagger
builder.Services.AddMassTransitConfiguration(builder.Configuration); // Extension method from Sportsbook.Infrastructure.MassTransit
builder.Services.AddQueueServices(); // Extension method from Sportsbook.API.QueueService
builder.Services.AddRedisCacheConfiguration(builder.Configuration); // Extension method from Sportsbook.Data.Redis
builder.Services.AddFluentValidationsConfiguration(); // Extension method from Sportsbook.API.Common

// Use Services
var app = builder.Build();
app.UseExceptionHandlerConfiguration(); // Extension method from Sportsbook.Infrastructure.ExceptionHandler
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(); // Extension method from Sportsbook.Infrastructure.Swagger
    //app.UseDeveloperExceptionPage();
}
if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}
app.UseRouting();
app.MapControllers();

await app.RunAsync();