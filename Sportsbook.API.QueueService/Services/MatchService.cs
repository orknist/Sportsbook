using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Sportsbook.API.Common.Requests;
using Sportsbook.API.Common.Responses;
using Sportsbook.API.QueueService.Interfaces;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.Infrastructure.Redis;

namespace Sportsbook.API.QueueService.Services
{
    public class MatchService : IMatchService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly RedisConfig _redisConfig;

        public MatchService(IBus bus, IMapper mapper, IDistributedCache cache, IOptions<RedisConfig> redisConfig)
        {
            _bus = bus;
            _mapper = mapper;
            _cache = cache;
            _redisConfig = redisConfig.Value;
        }

        public async Task<AddMatchApiResponse> AddMatchAsync(AddMatchApiRequest request)
        {
            await _cache.RemoveAsync("match:all");

            var messageRequest = _mapper.Map<AddMatchMessageRequest>(request);
            var requestClient = _bus.CreateRequestClient<AddMatchMessageRequest>();
            var messageResponse = await requestClient.GetResponse<AddMatchMessageResponse>(messageRequest);
            var apiResponse = _mapper.Map<AddMatchApiResponse>(messageResponse.Message);
            return apiResponse;
        }

        public async Task<GetMatchesApiResponse> GetMatchesAsync(GetMatchesApiRequest request)
        {
            return await _cache.GetThenSetAsync("match:all", async () =>
            {
                var messageRequest = _mapper.Map<GetMatchesMessageRequest>(request);
                var requestClient = _bus.CreateRequestClient<GetMatchesMessageRequest>();
                var messageResponse = await requestClient.GetResponse<GetMatchesMessageResponse>(messageRequest);
                var apiResponse = _mapper.Map<GetMatchesApiResponse>(messageResponse.Message);
                apiResponse._CachedAt = DateTime.Now;
                return apiResponse;
            }, new() { AbsoluteExpirationRelativeToNow = _redisConfig.AbsoluteExpiration });
        }

        public async Task<GetMatchApiResponse> GetMatchByIdAsync(GetMatchApiRequest request)
        {
            return await _cache.GetThenSetAsync($"match:{request.MatchId}", async () =>
            {
                var messageRequest = _mapper.Map<GetMatchMessageRequest>(request);
                var requestClient = _bus.CreateRequestClient<GetMatchMessageRequest>();
                var messageResponse = await requestClient.GetResponse<GetMatchMessageResponse>(messageRequest);
                var apiResponse = _mapper.Map<GetMatchApiResponse>(messageResponse.Message);
                apiResponse._CachedAt = DateTime.Now;
                return apiResponse;
            }, new() { AbsoluteExpirationRelativeToNow = _redisConfig.AbsoluteExpiration });
        }

        public async Task<DeleteMatchApiResponse> DeleteMatchByIdAsync(DeleteMatchApiRequest request)
        {
            await _cache.RemoveAsync("match:all");
            await _cache.RemoveAsync($"match:{request.MatchId}");

            var messageRequest = _mapper.Map<DeleteMatchMessageRequest>(request);
            var requestClient = _bus.CreateRequestClient<DeleteMatchMessageRequest>();
            var messageResponse = await requestClient.GetResponse<DeleteMatchMessageResponse>(messageRequest);
            var apiResponse = _mapper.Map<DeleteMatchApiResponse>(messageResponse.Message);
            return apiResponse;
        }
    }
}
