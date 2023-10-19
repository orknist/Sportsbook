using Sportsbook.Infrastructure.MassTransit;
using Sportsbook.Data.Dapper;
using Sportsbook.MatchConsumer.Business.Service;

// Create Builder
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Add Services
builder.Services.AddMassTransitConfiguration(builder.Configuration); // Extension method from Sportsbook.Infrastructure.MassTransit
builder.Services.AddRepositories(builder.Configuration); // Extension method from Sportsbook.Data.Dapper
builder.Services.AddAutoMapper(typeof(Sportsbook.MatchConsumer.Business.Mapping.MappingProfile).Assembly);
builder.Services.AddScoped<IMatchConsumerService, DefaultMatchConsumerService>();

// Use Services
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDatabaseInitializer(); // Extension method from Sportsbook.Data.Dapper
}

await app.RunAsync();