using MassTransit;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.MatchConsumer.Business.Service;

namespace Sportsbook.MatchConsumer.Consumers
{
    public class DeleteMatchConsumer : IConsumer<DeleteMatchRequestModel>
    {
        private readonly ILogger<GetMatchConsumer> _logger;
        private readonly IMatchConsumerService _matchConsumerService;

        public DeleteMatchConsumer(ILogger<GetMatchConsumer> logger, IMatchConsumerService matchConsumerService)
        {
            _logger = logger;
            _matchConsumerService = matchConsumerService;
        }

        public async Task Consume(ConsumeContext<DeleteMatchRequestModel> context)
        {
            _logger.LogInformation($"Received DeleteMatch request with id {context.Message.MatchId}.");

            try
            {
                var result = await _matchConsumerService.DeleteMatchAsync(context.Message);
                await context.RespondAsync(result);
                _logger.LogInformation($"DeleteMatch result respond.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while processing GetMatches request.");
                await context.RespondAsync(new BaseResponseModel { IsSuccess = false, ErrorMessage = exception.Message });
            }
        }
    }
}
