using AutoMapper;
using Sportsbook.API.Common.Models;
using Sportsbook.API.Common.Requests;
using Sportsbook.API.Common.Responses;
using Sportsbook.Contracts.Models;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;

namespace Sportsbook.API.QueueService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Sportsbook.API.Common.Requests >> Sportsbook.Contracts.Requests
            CreateMap<AddMatchApiRequest, AddMatchMessageRequest>();
            CreateMap<GetMatchesApiRequest, GetMatchesMessageRequest>();
            CreateMap<GetMatchApiRequest, GetMatchMessageRequest>();
            CreateMap<DeleteMatchApiRequest, DeleteMatchMessageRequest>();

            // Sportsbook.API.Common.Models << >> Sportsbook.Contracts.Models
            CreateMap<CompetitionApiModel, CompetitionMessageModel>().ReverseMap();
            CreateMap<CompetitorApiModel, CompetitorMessageModel>().ReverseMap();
            CreateMap<HeaderApiModel, HeaderMessageModel>().ReverseMap();
            CreateMap<MatchApiModel, MatchMessageModel>().ReverseMap();
            CreateMap<RoundApiModel, RoundMessageModel>().ReverseMap();
            CreateMap<SportApiModel, SportMessageModel>().ReverseMap();
            CreateMap<VenueApiModel, VenueMessageModel>().ReverseMap();

            // Sportsbook.Contracts.Responses >> Sportsbook.API.Common.Responses
            CreateMap<AddMatchMessageResponse, AddMatchApiResponse>();
            CreateMap<GetMatchesMessageResponse, GetMatchesApiResponse>();
            CreateMap<GetMatchMessageResponse, GetMatchApiResponse>();
            CreateMap<DeleteMatchMessageResponse, DeleteMatchApiResponse>();
        }
    }
}
