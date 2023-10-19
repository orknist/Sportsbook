using AutoMapper;
using Sportsbook.Contracts.Models;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.Data.Dapper.Entities;
using Sportsbook.Data.Dapper.Interfaces;

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

        public async Task<AddMatchResponseModel> AddMatchAsync(AddMatchRequestModel requestModel)
        {
            var entity = _mapper.Map<Match>(requestModel.Match);
            await _matchRepository.AddMatchAsync(entity);
            var responseModel = new AddMatchResponseModel { MatchId = entity.Id, IsSuccess = entity.Id > 0 };
            if (responseModel.IsSuccess == false)
                responseModel.ErrorMessage = $"Error while adding match";
            return responseModel;
        }

        public async Task<DeleteMatchResponseModel> DeleteMatchAsync(DeleteMatchRequestModel requestModel)
        {
            var result = await _matchRepository.DeleteMatchAsync(requestModel.MatchId);
            var responseModel = new DeleteMatchResponseModel { IsSuccess = result };
            if (result == false)
                responseModel.ErrorMessage = $"Match with id {requestModel.MatchId} not found";
            return responseModel;
        }

        public async Task<GetMatchResponseModel> GetMatchAsync(GetMatchRequestModel requestModel)
        {
            var entity = await _matchRepository.GetMatchByIdAsync(requestModel.MatchId);
            var model = _mapper.Map<MatchModel>(entity);
            var responseModel = new GetMatchResponseModel { Match = model, IsSuccess = model != null };
            if (responseModel.IsSuccess == false)
                responseModel.ErrorMessage = $"Match with id {requestModel.MatchId} not found";
            return responseModel;
        }

        public async Task<GetMatchesResponseModel> GetMatchesAsync(GetMatchesRequestModel requestModel)
        {
            var entities = await _matchRepository.GetAllMatchesAsync();
            var models = _mapper.Map<List<MatchModel>>(entities);
            var responseModel = new GetMatchesResponseModel { Matches = models, IsSuccess = models != null };
            if (responseModel.IsSuccess == false)
                responseModel.ErrorMessage = $"No matches found";
            return responseModel;
        }
    }
}
