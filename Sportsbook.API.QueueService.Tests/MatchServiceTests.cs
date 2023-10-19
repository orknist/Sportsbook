using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Moq;
using Sportsbook.API.Common.DTOs;
using Sportsbook.API.Common.DTOs.Shared;
using Sportsbook.API.QueueService.Services;
using Sportsbook.Contracts.Requests;
using Sportsbook.Data.Dapper.Interfaces;
using Sportsbook.Data.Redis;
using System.Text;

namespace Sportsbook.API.QueueService.Tests
{
    public class MatchServiceTests
    {
        // Create test for GetMatchByIdAsync method
        // Create new test method, which named GetMatchByIdAsync_ShouldGetMatchById
        // Arrange: Create mock objects and dependencies for test
        // Act: Call the method to be tested
        // Assert: Check the result of the method
        [Fact]
        public async Task GetMatchByIdAsync_ShouldGetMatchById()
        {
            // Arrange: Create mock objects and dependencies for test
            var mockBus = new Mock<IBus>();

            var mockMapper = new Mock<IMapper>();
            // Setup mockMapper.Map method for that line "var requestModel = _mapper.Map<GetMatchRequestModel>(dto);"
            //mockMapper.Setup(mockMapper => mockMapper.Map<GetMatchRequestModel>(It.IsAny<GetMatchDTO>())).Returns(new GetMatchRequestModel());

            // GetMatchDTO > GetMatchRequestModel
            var mockDestination = new GetMatchRequestModel(); // Bu, Map metodu tarafından döndürülecek mock nesnesi
            mockMapper.Setup(m => m.Map<GetMatchRequestModel>(It.IsAny<GetMatchDTO>())).Returns(mockDestination);



            var mockCache = new Mock<IDistributedCache>();
            //byte[] mockValue = Encoding.UTF8.GetBytes("mockValue");
            mockCache.Setup(m => m.GetAsync("match:1", It.IsAny<CancellationToken>())).ReturnsAsync((byte[]?)null);
            mockCache.Setup(m => m.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()));


            var mockOptions = new Mock<IOptions<RedisConfig>>();
            mockOptions.Setup(o => o.Value).Returns(new RedisConfig() { ConnectionString = "localhost", AbsoluteExpiration = TimeSpan.FromSeconds(5) });
            //mockOptions.SetupSet(o => o.Value.AbsoluteExpiration = It.IsAny<TimeSpan>()).Verifiable();

            var service = new MatchService(mockBus.Object, mockMapper.Object, mockCache.Object, mockOptions.Object);

            // Act: Call the method to be tested
            var result = await service.GetMatchByIdAsync(new GetMatchDTO() { MatchId = 1 });

            // Assert: Check the result of the method
            Assert.NotNull(result);
        }

        // Create new test method, which named AddMatchAsync_ShouldAddMatch
        // Arrange: Create mock objects and dependencies for test
        // Act: Call the method to be tested
        // Assert: Check the result of the method
        [Fact]
        public async Task AddMatchAsync_ShouldAddMatch()
        {
            // Arrange: Create mock objects and dependencies for test
            var mockBus = new Mock<IBus>();

            var mockMapper = new Mock<IMapper>();
            //mockMapper.Setup(m => m.Map<SomeType>(It.IsAny<OtherType>())).Returns(new SomeType());

            var mockCache = new Mock<IDistributedCache>();

            var mockOptions = new Mock<IOptions<RedisConfig>>();
            mockOptions.Setup(o => o.Value).Returns(new RedisConfig() { });
            mockOptions.SetupSet(o => o.Value.AbsoluteExpiration = It.IsAny<TimeSpan>()).Verifiable();




            var service = new MatchService(mockBus.Object, mockMapper.Object, mockCache.Object, mockOptions.Object);

            // Act: Call the method to be tested
            var result = await service.AddMatchAsync(new AddMatchDTO());

            // Assert: Check the result of the method
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task AddMatchAsync_ShouldAddMatch()
        //{
        //    var mockBus = new Mock<IBus>();
        //    var mockMapper = new Mock<IMapper>();
        //    var mockCache = new Mock<IDistributedCache>();
        //    var mockOptions = new Mock<IOptions<RedisConfig>>();

        //    var service = new MatchService(mockBus.Object, mockMapper.Object, mockCache.Object, mockOptions.Object);

        //    // Arrange: Test için gerekli olan nesnelerin ve bağımlılıkların oluşturulması
        //    var mockRepository = new Mock<IMatchRepository>();
        //    mockRepository.Setup(repo => repo.AddMatchAsync(It.IsAny<Data.Dapper.Entities.Match>())).ReturnsAsync(new Data.Dapper.Entities.Match());
        //    var service = new MatchService(mockRepository.Object);

        //    // Act: Test edilmek istenen metodun çağrılması
        //    var result = await service.AddMatchAsync(new MatchDTO());

        //    // Assert: Sonucun doğruluğunun kontrol edilmesi
        //    Assert.NotNull(result);
        //}
    }
}