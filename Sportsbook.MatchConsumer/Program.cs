using Sportsbook.Data.Dapper;
using Sportsbook.Infrastructure.Dapper;
using Sportsbook.Infrastructure.MassTransit;
using Sportsbook.MatchConsumer.Business;

// Create Builder
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

// Add Services
builder.Services.AddMassTransitConfiguration(builder.Configuration); // Extension method from Sportsbook.Infrastructure.MassTransit
builder.Services.AddDapperDatabaseConfiguration(builder.Configuration); // Extension method from Sportsbook.Infrastructure.Dapper
builder.Services.AddRepositories(); // Extension method from Sportsbook.Data.Dapper
builder.Services.AddMatchConsumerConfiguration(); // Extension method from Sportsbook.MatchConsumer.Business

// Use Services
var app = builder.Build();
app.UseDapperDatabaseInitializer(); // Extension method from Sportsbook.Infrastructure.Dapper

await app.RunAsync();