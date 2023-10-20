using AutoMapper;
using Sportsbook.Data.Dapper.Entities;
using Sportsbook.Data.Entities;

namespace Sportsbook.Data.Dapper.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Sportsbook.Data.Entities << >> Sportsbook.Data.Dapper.Entities
            CreateMap<CompetitionEntity, CompetitionDapperEntity>().ReverseMap();
            CreateMap<CompetitorEntity, CompetitorDapperEntity>().ReverseMap();
            CreateMap<MatchEntity, MatchDapperEntity>().ReverseMap();
            CreateMap<RoundEntity, RoundDapperEntity>().ReverseMap();
            CreateMap<SportEntity, SportDapperEntity>().ReverseMap();
            CreateMap<VenueEntity, VenueDapperEntity>().ReverseMap();
        }
    }
}
