namespace Sportsbook.Contracts.Responses
{
    public class BaseResponseModel
    {
        public bool? IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
