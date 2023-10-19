namespace Sportsbook.API.Common.DTOs
{
    public class BaseResultDTO
    {
        public bool? IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
