namespace Sportsbook.Contracts.Responses
{
    public class BaseMessageResponse
    {
        public bool? IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
