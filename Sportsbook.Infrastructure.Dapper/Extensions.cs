using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Sportsbook.Infrastructure.Dapper
{
    public static class Extensions
    {
        public static IServiceCollection AddDapperDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)));

            services.AddSingleton<IDbConnection>(provider =>
            {
                var config = configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
                return new SqliteConnection(config.ConnectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseDapperDatabaseInitializer(this IApplicationBuilder app)
        {
            var dbConnection = app.ApplicationServices.GetRequiredService<IDbConnection>();
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();

            dbConnection.Execute(@"CREATE TABLE ""Rounds"" (""Id"" INT PRIMARY KEY,""Name"" NVARCHAR(255));");
            dbConnection.Execute(@"CREATE TABLE ""Sports"" (""Id"" INT PRIMARY KEY,""Name"" NVARCHAR(255));");
            dbConnection.Execute(@"CREATE TABLE ""Venues"" (""Id"" INT PRIMARY KEY,""Name"" NVARCHAR(255));");
            dbConnection.Execute(@"CREATE TABLE ""Competitions"" (""Id"" INT PRIMARY KEY,""Name"" NVARCHAR(255));");
            dbConnection.Execute(@"CREATE TABLE ""Matches"" (""Id"" INT PRIMARY KEY,""Name"" NVARCHAR(255),""RoundId"" INT,""SportId"" INT,""VenueId"" INT,""Status"" NVARCHAR(50),""CompetitionId"" INT,""StartTimeUtc"" DATETIME);");
            dbConnection.Execute(@"CREATE TABLE ""Competitors"" (""Id"" INT PRIMARY KEY,""MatchId"" INT,""Name"" NVARCHAR(255),""HomeAway"" NVARCHAR(50),""CompetitorType"" NVARCHAR(50));");

            dbConnection.Execute(@"INSERT INTO ""Rounds"" (""Id"", ""Name"") VALUES (1, 'Final');");
            dbConnection.Execute(@"INSERT INTO ""Rounds"" (""Id"", ""Name"") VALUES (2, 'Semi-Final');");
            dbConnection.Execute(@"INSERT INTO ""Rounds"" (""Id"", ""Name"") VALUES (3, 'Quarter-Final');");
            dbConnection.Execute(@"INSERT INTO ""Sports"" (""Id"", ""Name"") VALUES (1, 'Football');");
            dbConnection.Execute(@"INSERT INTO ""Sports"" (""Id"", ""Name"") VALUES (2, 'Basketball');");
            dbConnection.Execute(@"INSERT INTO ""Venues"" (""Id"", ""Name"") VALUES (1, 'Ataturk Olympic Stadium');");
            dbConnection.Execute(@"INSERT INTO ""Competitions"" (""Id"", ""Name"") VALUES (1, 'World Cup');");
            dbConnection.Execute(@"INSERT INTO ""Competitions"" (""Id"", ""Name"") VALUES (2, 'Euro Cup');");
            dbConnection.Execute(@"INSERT INTO ""Matches"" (""Id"", ""Name"", ""RoundId"", ""SportId"", ""VenueId"", ""Status"", ""CompetitionId"", ""StartTimeUtc"") VALUES (1, 'Turkey v England', 1, 1, 1, 'Scheduled', 1, '2026-07-19T21:00:00Z');");
            dbConnection.Execute(@"INSERT INTO ""Competitors"" (""Id"", ""MatchId"", ""Name"", ""HomeAway"", ""CompetitorType"") VALUES (1, 1, 'Turkey', 'Home', 'Team');");
            dbConnection.Execute(@"INSERT INTO ""Competitors"" (""Id"", ""MatchId"", ""Name"", ""HomeAway"", ""CompetitorType"") VALUES (2, 1, 'England', 'Away', 'Team');");

            return app;
        }
    }
}
