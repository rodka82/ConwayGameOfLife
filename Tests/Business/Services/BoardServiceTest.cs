using ConwayGameOfLife.Business.Services;
using ConwayGameOfLife.Business.Services.Interfaces;
using ConwayGameOfLife.Infra.Data.Interfaces;
using Domain.Entities;
using Moq;

namespace ConwayGameOfLife.Tests.Business.Services
{
    public class BoardServiceTest
    {
        private string _serializedInitialState = @"[
                [0, 1, 0, 0, 0, 0, 0, 1, 1, 1],
                [0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 1, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0, 1, 0],
                [0, 1, 1, 1, 1, 0, 0, 0, 1, 0],
                [0, 0, 1, 0, 1, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 1, 1, 0, 1, 0, 1],
                [0, 0, 0, 0, 0, 0, 1, 0, 0, 0],
                [0, 1, 0, 0, 0, 1, 1, 1, 1, 0]
                ]";

        public class TheAddBoardMethod : BoardServiceTest
        {
            [Fact]
            public async Task ShouldReturnBoardIdAfterAddingBoard()
            {
                var expectedBoardId = 1;
                var boardRepositoryMock = new Mock<IBoardRepository>();
                boardRepositoryMock
                    .Setup(repo => repo.AddBoardAsync(It.IsAny<Board>()))
                    .ReturnsAsync(expectedBoardId);
                var boardManagerMock = new Mock<IBoardManager>();

                var boardService = new BoardService(boardManagerMock.Object, boardRepositoryMock.Object);
                var result = await boardService.AddBoardAsync(_serializedInitialState);

                Assert.Equal(expectedBoardId, result);
            }
        }

        public class TheGetNextStateMethod : BoardServiceTest
        {
            [Fact]
            public async Task ShouldReturnNextStateGrid()
            {
                var boardId = 1;
                var board = new Board(_serializedInitialState);
                var expectedGrid = new int[3, 3];

                var boardRepositoryMock = new Mock<IBoardRepository>();
                boardRepositoryMock.Setup(repo => repo.GetBoardByIdAsync(boardId)).ReturnsAsync(board);
                var boardManagerMock = new Mock<IBoardManager>();
                boardManagerMock.Setup(manager => manager.CalculateNextGridState(board)).Callback(() =>
                {
                    string serializedNextState = @"[
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
                            ]";

                });

                var boardService = new BoardService(boardManagerMock.Object, boardRepositoryMock.Object);
                var result = await boardService.CalculateNextStateAsync(boardId);

                Assert.Equal(board.State, result);
            }
        }

        public class TheGetStateXStepsAwayMethod : BoardServiceTest
        {
            [Fact]
            public async Task ShouldReturnGridStateAfterXSteps()
            {
                var boardId = 1;
                var xSteps = 5;
                var board = new Board(_serializedInitialState);

                var boardRepositoryMock = new Mock<IBoardRepository>();
                boardRepositoryMock.Setup(repo => repo.GetBoardByIdAsync(boardId)).ReturnsAsync(board);
                var boardManagerMock = new Mock<IBoardManager>();
                boardManagerMock.Setup(manager => manager.CalculateStateXStepsAway(board, xSteps)).Callback(() =>
                {
                    string serializedNextState = @"[
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0],
                            [1, 0, 1, 0, 1, 0, 1, 0, 1, 0]
                            ]";

                });

                var boardService = new BoardService(boardManagerMock.Object, boardRepositoryMock.Object);
                var result = await boardService.GetStateXStepsAwayAsync(boardId, xSteps);

                Assert.Equal(board.State, result);
            }
        }

        public class TheGetFinalStateMethod : BoardServiceTest
        {
            [Fact]
            public async Task ShouldReturnFinalStateGrid()
            {
                var boardId = 1;
                var xSteps = 5;
                var board = new Board(_serializedInitialState);

                var boardRepositoryMock = new Mock<IBoardRepository>();
                boardRepositoryMock.Setup(repo => repo.GetBoardByIdAsync(boardId)).ReturnsAsync(board);
                var boardManagerMock = new Mock<IBoardManager>();
                boardManagerMock.Setup(manager => manager.CalculateFinalState(board, xSteps)).Callback(() =>
                {
                    string serializedNextState = @"[
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
                            ]";

                });

                var boardService = new BoardService(boardManagerMock.Object, boardRepositoryMock.Object);
                var result = await boardService.CalculateFinalStateAsync(boardId, boardId);

                Assert.Equal(board.State, result);
            }
        }
    }
}