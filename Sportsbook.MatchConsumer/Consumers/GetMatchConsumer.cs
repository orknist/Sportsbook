using MassTransit;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.MatchConsumer.Business.Service;

namespace Sportsbook.MatchConsumer.Consumers
{
    public class GetMatchConsumer : IConsumer<GetMatchRequestModel>
    {
        private readonly ILogger<GetMatchConsumer> _logger;
        private readonly IMatchConsumerService _matchConsumerService;

        public GetMatchConsumer(ILogger<GetMatchConsumer> logger, IMatchConsumerService matchConsumerService)
        {
            _logger = logger;
            _matchConsumerService = matchConsumerService;
        }

        public async Task Consume(ConsumeContext<GetMatchRequestModel> context)
        {
            _logger.LogInformation($"Received GetMatch request with id {context.Message.MatchId}.");

            try
            {
                var result = await _matchConsumerService.GetMatchAsync(context.Message);
                await context.RespondAsync(result);
                _logger.LogInformation($"GetMatch Result responded.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while processing GetMatches request.");
                await context.RespondAsync(new BaseResponseModel { IsSuccess = false, ErrorMessage = exception.Message });
            }
        }
    }
}
