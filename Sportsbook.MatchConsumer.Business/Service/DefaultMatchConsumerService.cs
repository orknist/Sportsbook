using AutoMapper;
using Sportsbook.Contracts.Models;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.Data.Entities;
using Sportsbook.Data.Repositories;

namespace Sportsbook.MatchConsumer.Business.Service
{
    public class DefaultMatchConsumerService : IMatchConsumerService
    {
        private readonly IMapper _mapper;
        private readonly IMatchRepository _matchRepository;

        public DefaultMatchConsumerService(IMapper mapper, IMatchRepository matchRepository)
        {
            _mapper = mapper;
            _matchRepository = matchRepository;
        }

        public async Task<AddMatchMessageResponse> AddMatchAsync(AddMatchMessageRequest requestModel)
        {
            var entity = _mapper.Map<MatchEntity>(requestModel.Match);
            await _matchRepository.AddMatchAsync(entity);
            var responseModel = new AddMatchMessageResponse { MatchId = entity.Id, IsSuccess = entity.Id > 0 };
            if (responseModel.IsSuccess == false)
                responseModel.ErrorMessage = $"Error while adding match";
            return responseModel;
        }

        public async Task<DeleteMatchMessageResponse> DeleteMatchAsync(DeleteMatchMessageRequest requestModel)
        {
            var result = await _matchRepository.DeleteMatchAsync(requestModel.MatchId);
            var responseModel = new DeleteMatchMessageResponse { IsSuccess = result };
            if (result == false)
                responseModel.ErrorMessage = $"Match with id {requestModel.MatchId} not found";
            return responseModel;
        }

        public async Task<GetMatchMessageResponse> GetMatchAsync(GetMatchMessageRequest requestModel)
        {
            var entity = await _matchRepository.GetMatchByIdAsync(requestModel.MatchId);
            var model = _mapper.Map<MatchMessageModel>(entity);
            var responseModel = new GetMatchMessageResponse { Match = model, IsSuccess = model != null };
            if (responseModel.IsSuccess == false)
                responseModel.ErrorMessage = $"Match with id {requestModel.MatchId} not found";
            return responseModel;
        }

        public async Task<GetMatchesMessageResponse> GetMatchesAsync(GetMatchesMessageRequest requestModel)
        {
            var entities = await _matchRepository.GetAllMatchesAsync();
            var models = _mapper.Map<List<MatchMessageModel>>(entities);
            var responseModel = new GetMatchesMessageResponse { Matches = models, IsSuccess = models != null };
            if (responseModel.IsSuccess == false)
                responseModel.ErrorMessage = $"No matches found";
            return responseModel;
        }
    }
}
