using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Sportsbook.API.Common.DTOs;
using Sportsbook.API.QueueService.Interfaces;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.Data.Redis;

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

        public async Task<AddMatchResultDTO> AddMatchAsync(AddMatchDTO dto)
        {
            await _cache.RemoveAsync("match:all");

            var requestModel = _mapper.Map<AddMatchRequestModel>(dto);
            var requestClient = _bus.CreateRequestClient<AddMatchRequestModel>();
            var response = await requestClient.GetResponse<AddMatchResponseModel>(requestModel);
            var responseDTO = _mapper.Map<AddMatchResultDTO>(response.Message);
            return responseDTO;
        }

        public async Task<GetMatchesResultDTO> GetMatchesAsync(GetMatchesDTO dto)
        {
            return await _cache.GetThenSetAsync("match:all", async () =>
            {
                var requestModel = _mapper.Map<GetMatchesRequestModel>(dto);
                var requestClient = _bus.CreateRequestClient<GetMatchesRequestModel>();
                var response = await requestClient.GetResponse<GetMatchesResponseModel>(requestModel);
                var responseDTO = _mapper.Map<GetMatchesResultDTO>(response.Message);
                responseDTO._CachedAt = DateTime.Now;
                return responseDTO;
            }, new() { AbsoluteExpirationRelativeToNow = _redisConfig.AbsoluteExpiration });
        }

        public async Task<GetMatchResultDTO> GetMatchByIdAsync(GetMatchDTO dto)
        {
            return await _cache.GetThenSetAsync($"match:{dto.MatchId}", async () =>
            {
                var requestModel = _mapper.Map<GetMatchRequestModel>(dto);
                var requestClient = _bus.CreateRequestClient<GetMatchRequestModel>();
                var response = await requestClient.GetResponse<GetMatchResponseModel>(requestModel);
                var responseDTO = _mapper.Map<GetMatchResultDTO>(response.Message);
                responseDTO._CachedAt = DateTime.Now;
                return responseDTO;
            }, new() { AbsoluteExpirationRelativeToNow = _redisConfig.AbsoluteExpiration });
        }

        public async Task<DeleteMatchResultDTO> DeleteMatchByIdAsync(DeleteMatchDTO dto)
        {
            await _cache.RemoveAsync("match:all");
            await _cache.RemoveAsync($"match:{dto.MatchId}");

            var requestModel = _mapper.Map<DeleteMatchRequestModel>(dto);
            var requestClient = _bus.CreateRequestClient<DeleteMatchRequestModel>();
            var response = await requestClient.GetResponse<DeleteMatchResponseModel>(requestModel);
            var responseDTO = _mapper.Map<DeleteMatchResultDTO>(response.Message);
            return responseDTO;
        }
    }
}
