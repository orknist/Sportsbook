using AutoMapper;
using Sportsbook.Contracts.Models;
using Sportsbook.Data.Dapper.Entities;

namespace Sportsbook.MatchConsumer.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Sportsbook.Data.Dapper.Entities << >> Sportsbook.Contracts.Models
            CreateMap<Competition, CompetitionModel>().ReverseMap();
            CreateMap<Competitor, CompetitorModel>().ReverseMap();
            CreateMap<Match, MatchModel>().ReverseMap();
            CreateMap<Round, RoundModel>().ReverseMap();
            CreateMap<Sport, SportModel>().ReverseMap();
            CreateMap<Venue, VenueModel>().ReverseMap();
        }
    }
}
