using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.API.Common.Responses;
using Sportsbook.Contracts.Models;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.Infrastructure.MassTransit;
using Sportsbook.TestBase;

namespace Sportsbook.MatchConsumer.Tests
{
    public class IntegrationTests
    {
        private readonly IBusControl _bus;
        private readonly IMapper _mapper;

        protected internal IConfigurationRoot _configuration { get; set; }
        private readonly ServiceProvider _serviceProvider;

        public IntegrationTests()
        {
            _mapper = AutoMapperFactory.Instance;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMassTransitConfiguration(_configuration); // Extension method from Sportsbook.Infrastructure.MassTransit
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _bus = _serviceProvider.GetRequiredService<IBusControl>();
            _bus.Start(); // Must call, otherwise message is transmitting but response is not receiving
        }

        [Fact]
        public async Task GetMatch_WhenMatchIdExists_ReturnsMatch()
        {
            var messageRequest = new GetMatchMessageRequest(default) { MatchId = 1 };
            var requestClient = _bus.CreateRequestClient<GetMatchMessageRequest>();
            var messageResponse = await requestClient.GetResponse<GetMatchMessageResponse>(messageRequest);
            var apiResponse = _mapper.Map<GetMatchApiResponse>(messageResponse.Message);

            Assert.NotNull(apiResponse);
            Assert.True(apiResponse.IsSuccess, "apiResponse.IsSuccess is false");
            Assert.NotNull(apiResponse.Match);
            Assert.Equal(1, apiResponse.Match.Id);
        }

        [Fact]
        public async Task GetMatch_WhenMatchIdNotExists_ReturnsNoMatch()
        {
            var messageRequest = new GetMatchMessageRequest(default) { MatchId = 10 };
            var requestClient = _bus.CreateRequestClient<GetMatchMessageRequest>();
            var messageResponse = await requestClient.GetResponse<GetMatchMessageResponse>(messageRequest);
            var apiResponse = _mapper.Map<GetMatchApiResponse>(messageResponse.Message);

            Assert.NotNull(apiResponse);
            Assert.False(apiResponse.IsSuccess, "apiResponse.IsSuccess is true");
            Assert.Null(apiResponse.Match);
        }

        [Fact]
        public async Task AddMatch_WhenMatchIdNotExists_ReturnsMatchId()
        {
            var messageRequest = new AddMatchMessageRequest(
                new MatchMessageModel(
                    (int)DateTime.UtcNow.Ticks,
                    "Turkey v England",
                    new RoundMessageModel(1, "Final"),
                    new SportMessageModel(1, "Football"),
                    new VenueMessageModel(1, "Ataturk Olympic Stadium"),
                    "Scheduled",
                    new CompetitionMessageModel(1, "World Cup"),
                    new List<CompetitorMessageModel>
                    {
                        new (1, "Turkey", "Home", "Team", default, default),
                        new (2, "England", "Away", "Team", default, default)
                    },
                    new DateTime(2026, 7, 19, 21, 0, 0)),
                new HeaderMessageModel(DateTime.UtcNow));

            var requestClient = _bus.CreateRequestClient<AddMatchMessageRequest>();
            var messageResponse = await requestClient.GetResponse<AddMatchMessageResponse>(messageRequest);
            var apiResponse = _mapper.Map<AddMatchApiResponse>(messageResponse.Message);

            Assert.NotNull(apiResponse);
            Assert.False(apiResponse.IsSuccess, "apiResponse.IsSuccess is true");
            Assert.Equal(messageRequest.Match.Id, apiResponse.MatchId);
        }
    }
}