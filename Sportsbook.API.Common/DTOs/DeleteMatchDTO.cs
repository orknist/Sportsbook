using FluentValidation;

namespace Sportsbook.API.Common.DTOs
{
    public record DeleteMatchDTO(int MatchId);

    public class DeleteMatchDTOValidator : AbstractValidator<DeleteMatchDTO>
    {
        public DeleteMatchDTOValidator()
        {
            RuleFor(x => x.MatchId).NotNull().GreaterThan(0);
        }
    }

    public class DeleteMatchResultDTO : BaseResultDTO { }
}
