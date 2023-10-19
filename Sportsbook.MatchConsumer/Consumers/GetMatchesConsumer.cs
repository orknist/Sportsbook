using MassTransit;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.MatchConsumer.Business.Service;

namespace Sportsbook.MatchConsumer.Consumers
{
    public class GetMatchesConsumer : IConsumer<GetMatchesRequestModel>
    {
        private readonly ILogger<GetMatchConsumer> _logger;
        private readonly IMatchConsumerService _matchConsumerService;

        public GetMatchesConsumer(ILogger<GetMatchConsumer> logger, IMatchConsumerService matchConsumerService)
        {
            _logger = logger;
            _matchConsumerService = matchConsumerService;
        }

        public async Task Consume(ConsumeContext<GetMatchesRequestModel> context)
        {
            _logger.LogInformation("Received GetMatches request.");

            try
            {
                var result = await _matchConsumerService.GetMatchesAsync(context.Message);
                await context.RespondAsync(result);
                _logger.LogInformation($"GetMatches result respond.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while processing GetMatches request.");
                await context.RespondAsync(new BaseResponseModel { IsSuccess = false, ErrorMessage = exception.Message });
            }
        }
    }
}
