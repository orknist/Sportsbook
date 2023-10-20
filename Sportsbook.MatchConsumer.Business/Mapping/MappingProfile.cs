using AutoMapper;
using Sportsbook.Contracts.Models;
using Sportsbook.Data.Entities;

namespace Sportsbook.MatchConsumer.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Sportsbook.Data.Entities << >> Sportsbook.Contracts.Models
            CreateMap<CompetitionEntity, CompetitionMessageModel>().ReverseMap();
            CreateMap<CompetitorEntity, CompetitorMessageModel>().ReverseMap();
            CreateMap<MatchEntity, MatchMessageModel>().ReverseMap();
            CreateMap<RoundEntity, RoundMessageModel>().ReverseMap();
            CreateMap<SportEntity, SportMessageModel>().ReverseMap();
            CreateMap<VenueEntity, VenueMessageModel>().ReverseMap();
        }
    }
}
