using FluentValidation;

namespace Sportsbook.API.Common.DTOs.Shared
{
    public record MatchDTO(
        int Id, 
        string Name, 
        RoundDTO Round, 
        SportDTO Sport, 
        VenueDTO Venue, 
        string Status, 
        CompetitionDTO Competition, 
        List<CompetitorDTO> Competitors, 
        DateTime StartTimeUtc);

    public class MatchDTOValidator : AbstractValidator<MatchDTO>
    {
        public MatchDTOValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Round).NotNull().SetValidator(new RoundDTOValidator());
            RuleFor(x => x.Sport).NotNull().SetValidator(new SportDTOValidator());
            RuleFor(x => x.Venue).NotNull().SetValidator(new VenueDTOValidator());
            RuleFor(x => x.Status).NotNull().NotEmpty();
            RuleFor(x => x.Competition).NotNull().SetValidator(new CompetitionDTOValidator());
            RuleFor(x => x.Competitors).NotNull().NotEmpty();
            RuleForEach(x => x.Competitors).SetValidator(new CompetitorDTOValidator());
            RuleFor(x => x.StartTimeUtc).NotNull().NotEmpty();
        }
    }
}
