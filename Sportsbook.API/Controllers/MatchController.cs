using Microsoft.AspNetCore.Mvc;
using Sportsbook.API.Common.DTOs;
using Sportsbook.API.QueueService.Interfaces;

namespace Sportsbook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMatch(AddMatchDTO dto)
        {
            var result = await _matchService.AddMatchAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMatches()
        {
            var result = await _matchService.GetMatchesAsync(new());
            return Ok(result);
        }

        [HttpGet("{MatchId}")]
        public async Task<IActionResult> GetMatchById([FromRoute] GetMatchDTO dto)
        {
            var result = await _matchService.GetMatchByIdAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{MatchId}")]
        public async Task<IActionResult> DeleteMatchById([FromRoute] DeleteMatchDTO dto)
        {
            var result = await _matchService.DeleteMatchByIdAsync(dto);
            return Ok(result);
        }
    }
}
