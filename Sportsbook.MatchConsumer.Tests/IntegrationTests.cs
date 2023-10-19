using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.API.Common.DTOs;
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
            var requestModel = new GetMatchRequestModel(default) { MatchId = 1 };
            var requestClient = _bus.CreateRequestClient<GetMatchRequestModel>();
            var response = await requestClient.GetResponse<GetMatchResponseModel>(requestModel);
            var responseDTO = _mapper.Map<GetMatchResultDTO>(response.Message);

            Assert.NotNull(responseDTO);
            Assert.True(responseDTO.IsSuccess, "responseDTO.IsSuccess is false");
            Assert.NotNull(responseDTO.Match);
            Assert.Equal(1, responseDTO.Match.Id);
        }

        [Fact]
        public async Task GetMatch_WhenMatchIdNotExists_ReturnsNoMatch()
        {
            var requestModel = new GetMatchRequestModel(default) { MatchId = 10 };
            var requestClient = _bus.CreateRequestClient<GetMatchRequestModel>();
            var response = await requestClient.GetResponse<GetMatchResponseModel>(requestModel);
            var responseDTO = _mapper.Map<GetMatchResultDTO>(response.Message);

            Assert.NotNull(responseDTO);
            Assert.False(responseDTO.IsSuccess, "responseDTO.IsSuccess is true");
            Assert.Null(responseDTO.Match);
        }

        [Fact]
        public async Task AddMatch_WhenMatchIdNotExists_ReturnsMatchId()
        {
            var requestModel = new AddMatchRequestModel(
                new MatchModel(
                    (int)DateTime.UtcNow.Ticks,
                    "Turkey v England",
                    new RoundModel(1, "Final"),
                    new SportModel(1, "Football"),
                    new VenueModel(1, "Ataturk Olympic Stadium"),
                    "Scheduled",
                    new CompetitionModel(1, "World Cup"),
                    new List<CompetitorModel>
                    {
                        new (1, "Turkey", "Home", "Team", default, default),
                        new (2, "England", "Away", "Team", default, default)
                    },
                    new DateTime(2026, 7, 19, 21, 0, 0)),
                new HeaderModel(DateTime.UtcNow));

            var requestClient = _bus.CreateRequestClient<AddMatchRequestModel>();
            var response = await requestClient.GetResponse<AddMatchResponseModel>(requestModel);
            var responseDTO = _mapper.Map<AddMatchResultDTO>(response.Message);

            Assert.NotNull(responseDTO);
            Assert.False(responseDTO.IsSuccess, "responseDTO.IsSuccess is true");
            Assert.Equal(requestModel.Match.Id, responseDTO.MatchId);
        }
    }
}