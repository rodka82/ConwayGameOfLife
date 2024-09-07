using ConwayGameOfLife.API.Binders;
using ConwayGameOfLife.API.Dtos;
using ConwayGameOfLife.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConwayGameOfLife.API.Controllers
{
    [ApiController]
    [Route("api/boards")]
    [Produces("application/json")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService) { 
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<ActionResult<BoardCreatedDto>> CreateBoard([ModelBinder(BinderType = typeof(InitialStateModelBinder))] string initialState)
        {
            if (string.IsNullOrEmpty(initialState))
                return BadRequest("Initial state cannot be null or empty.");

            var boardId = await _boardService.AddBoardAsync(initialState);
            var boardCreatedDto = new BoardCreatedDto { BoardId = boardId };

            return Created(string.Empty, boardCreatedDto);
        }

        [HttpGet("{boardId}/next")]
        public async Task<IActionResult> GetNextState(int boardId)
        {
            var nextState = await _boardService.CalculateNextStateAsync(boardId);
            if (nextState == null)
                return NotFound("Board not found.");

            var boardDto = new BoardDto { BoardState = nextState };

            return Ok(boardDto);
        }

        [HttpGet("{boardId}/steps/{xSteps}")]
        public async Task<IActionResult> GetXStatesAway(int boardId, int xSteps)
        {
            var state = await _boardService.GetStateXStepsAwayAsync(boardId, xSteps);
            if (state == null)
                return NotFound("Board not found or invalid steps.");

            var boardDto = new BoardDto { BoardState = state };

            return Ok(boardDto);
        }

        [HttpGet("{boardId}/final/{xSteps}")]
        public async Task<IActionResult> GetFinalState(int boardId, int xSteps)
        {
            var finalState = await _boardService.GetFinalStateAsync(boardId, xSteps);
            var boardDto = new BoardDto { BoardState = finalState };

            return Ok(boardDto);
        }
    }
}
