using AutoMapper;
using Sportsbook.API.Common.DTOs;
using Sportsbook.API.Common.DTOs.Shared;
using Sportsbook.Contracts.Models;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;

namespace Sportsbook.API.QueueService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Sportsbook.API.Common.DTOs >> Sportsbook.Contracts.Requests
            CreateMap<AddMatchDTO, AddMatchRequestModel>();
            CreateMap<GetMatchesDTO, GetMatchesRequestModel>();
            CreateMap<GetMatchDTO, GetMatchRequestModel>();
            CreateMap<DeleteMatchDTO, DeleteMatchRequestModel>();

            // Sportsbook.API.Common.DTOs.Shared << >> Sportsbook.Contracts.Models
            CreateMap<CompetitionDTO, CompetitionModel>().ReverseMap();
            CreateMap<CompetitorDTO, CompetitorModel>().ReverseMap();
            CreateMap<HeaderDTO, HeaderModel>().ReverseMap();
            CreateMap<MatchDTO, MatchModel>().ReverseMap();
            CreateMap<RoundDTO, RoundModel>().ReverseMap();
            CreateMap<SportDTO, SportModel>().ReverseMap();
            CreateMap<VenueDTO, VenueModel>().ReverseMap();

            // Sportsbook.Contracts.Responses >> Sportsbook.API.Common.DTOs
            CreateMap<AddMatchResponseModel, AddMatchResultDTO>();
            CreateMap<GetMatchesResponseModel, GetMatchesResultDTO>();
            CreateMap<GetMatchResponseModel, GetMatchResultDTO>();
            CreateMap<DeleteMatchResponseModel, DeleteMatchResultDTO>();
        }
    }
}
