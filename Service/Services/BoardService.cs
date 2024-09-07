using ConwayGameOfLife.Business.Interfaces;
using ConwayGameOfLife.Business.Services.Interfaces;
using ConwayGameOfLife.Infra.Data.Interfaces;
using Domain.Entities;

namespace ConwayGameOfLife.Business.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardManager _boardManager;
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardManager boardManager, IBoardRepository boardRepository) {
            _boardManager = boardManager;
            _boardRepository = boardRepository;
        }

        public async Task<int> AddBoardAsync(string initialState)
        {
            var board = new Board(initialState);
            int id = await _boardRepository.AddBoardAsync(board);
            return id;
        }

        public async Task<string> CalculateNextStateAsync(int boardId)
        {
            var board = await _boardRepository.GetBoardByIdAsync(boardId);
            _boardManager.CalculateNextGridState(board);
            return board.State;
        }

        public async Task<string> GetStateXStepsAwayAsync(int boardId, int xSteps)
        {
            var board = await _boardRepository.GetBoardByIdAsync(boardId);
            _boardManager.CalculateStateXStepsAway(board, xSteps);
            return board.State;
        }

        public async Task<string> GetFinalStateAsync(int boardId, int xSteps)
        {
            var board = await _boardRepository.GetBoardByIdAsync(boardId);
            _boardManager.CalculateFinalState(board, xSteps);
            return board.State;
        }
    }
}
