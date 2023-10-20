namespace Sportsbook.API.Common.Responses
{
    public class BaseApiResponse
    {
        public bool? IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
