using AutoMapper;
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
        private readonly IMapper _mapper;

        public BoardController(IBoardService boardService, IMapper mapper) { 
            _boardService = boardService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BoardCreatedDto>> CreateBoard([ModelBinder(BinderType = typeof(InitialStateModelBinder))] string initialState)
        {
            if (string.IsNullOrEmpty(initialState))
                return BadRequest("Initial state cannot be null or empty.");

            var boardId = await _boardService.AddBoardAsync(initialState);
            var boardCreatedDto = _mapper.Map<BoardCreatedDto>(boardId);

            return Created(string.Empty, boardCreatedDto);
        }

        [HttpGet("{boardId}/next")]
        public async Task<IActionResult> GetNextState(int boardId)
        {
            var nextState = await _boardService.CalculateNextStateAsync(boardId);
            if (nextState == null)
                return NotFound("Board not found.");

            var boardDto = _mapper.Map<BoardDto>(nextState);

            return Ok(boardDto);
        }

        [HttpGet("{boardId}/steps/{xSteps}")]
        public async Task<IActionResult> GetXStatesAway(int boardId, int xSteps)
        {
            var state = await _boardService.GetStateXStepsAwayAsync(boardId, xSteps);
            if (state == null)
                return NotFound("Board not found or invalid steps.");

            var boardDto = _mapper.Map<BoardDto>(state);

            return Ok(boardDto);
        }

        [HttpGet("{boardId}/final/{xSteps}")]
        public async Task<IActionResult> GetFinalState(int boardId, int xSteps)
        {
            var finalState = await _boardService.CalculateFinalStateAsync(boardId, xSteps);
            var boardDto = _mapper.Map<BoardDto>(finalState);

            return Ok(boardDto);
        }
    }
}
