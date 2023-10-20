using FluentValidation;

namespace Sportsbook.API.Common.Models
{
    public record MatchApiModel(
        int Id, 
        string Name, 
        RoundApiModel Round, 
        SportApiModel Sport,
        VenueApiModel Venue, 
        string Status, 
        CompetitionApiModel Competition, 
        List<CompetitorApiModel> Competitors, 
        DateTime StartTimeUtc);

    public class MatchApiModelValidator : AbstractValidator<MatchApiModel>
    {
        public MatchApiModelValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Round).NotNull().SetValidator(new RoundApiModelValidator());
            RuleFor(x => x.Sport).NotNull().SetValidator(new SportApiModelValidator());
            RuleFor(x => x.Venue).NotNull().SetValidator(new VenueApiModelValidator());
            RuleFor(x => x.Status).NotNull().NotEmpty();
            RuleFor(x => x.Competition).NotNull().SetValidator(new CompetitionApiModelValidator());
            RuleFor(x => x.Competitors).NotNull().NotEmpty();
            RuleForEach(x => x.Competitors).SetValidator(new CompetitorApiModelValidator());
            RuleFor(x => x.StartTimeUtc).NotNull().NotEmpty();
        }
    }
}
