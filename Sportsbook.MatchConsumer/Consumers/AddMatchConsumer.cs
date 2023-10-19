using MassTransit;
using Sportsbook.Contracts.Requests;
using Sportsbook.Contracts.Responses;
using Sportsbook.MatchConsumer.Business.Service;

namespace Sportsbook.MatchConsumer.Consumers
{
    public class AddMatchConsumer : IConsumer<AddMatchRequestModel>
    {
        private readonly ILogger<GetMatchConsumer> _logger;
        private readonly IMatchConsumerService _matchConsumerService;

        public AddMatchConsumer(ILogger<GetMatchConsumer> logger, IMatchConsumerService matchConsumerService)
        {
            _logger = logger;
            _matchConsumerService = matchConsumerService;
        }

        public async Task Consume(ConsumeContext<AddMatchRequestModel> context)
        {
            _logger.LogInformation($"Received AddMatch request with id {context.Message.Match.Id}.");

            try
            {
                var result = await _matchConsumerService.AddMatchAsync(context.Message);
                await context.RespondAsync(result);
                _logger.LogInformation($"AddMatch result respond.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while processing GetMatches request.");
                await context.RespondAsync(new BaseResponseModel { IsSuccess = false, ErrorMessage = exception.Message });
            }
        }
    }
}
