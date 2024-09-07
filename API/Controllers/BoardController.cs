using ConwayGameOfLife.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConwayGameOfLife.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService) { 
            _boardService = boardService;
        }

        [HttpPost("api/board")]
        public async Task<IActionResult> CreateBoard([FromBody] string initialState)
        {
            var boardId = await _boardService.AddBoardAsync(initialState);
            return Ok(new { BoardId = boardId });
        }

        [HttpGet("api/board/{boardId}/next")]
        public async Task<IActionResult> GetNextState(int boardId)
        {
            var nextState = await _boardService.CalculateNextStateAsync(boardId);
            return Ok(new { NextState = nextState });
        }

        [HttpGet("api/board/{boardId}/steps/{xSteps}")]
        public async Task<IActionResult> GetXStatesAway(int boardId, int xSteps)
        {
            var state = await _boardService.GetStateXStepsAwayAsync(boardId, xSteps);
            return Ok(new { NextState = state });
        }

        [HttpGet("api/board/{boardId}/final/{xSteps}")]
        public async Task<IActionResult> GetFinalState(int boardId, int xSteps)
        {
            var finalState = await _boardService.GetFinalStateAsync(boardId, xSteps);
           //if (finalState.IsStable)
                return Ok(finalState);
            //else
           //     return BadRequest("Board didn't stabilize after maximum attempts.");
        }
    }
}
