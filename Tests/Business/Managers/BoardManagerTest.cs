using ConwayGameOfLife.Business.Managers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife.Tests.Business.Managers
{
    public class BoardManagerTest
    {
        public class TheCalculateNextGridStateMethod : BoardManagerTest
        {
            [Fact]
            public void ShouldCorrectlyCalculateNextStateForLiveAndDeadCells()
            {
                string initialState = @"[[1,0,0],[0,1,0],[0,0,1]]";
                var board = new Board(initialState);
                var boardManager = new BoardManager();

                boardManager.CalculateNextGridState(board);

                var expectedState = @"[[0,0,0],[0,1,0],[0,0,0]]";
                Assert.Equal(board.State, expectedState);
            }
        }

        public class TheCalculateStateXStepsAwayMethod : BoardManagerTest
        {
            [Fact]
            public void ShouldCorrectlyCalculateStateAfterXSteps()
            {
                string initialState = @"[[0,1,0],[0,0,1],[1,1,1]]";
                var board = new Board(initialState);

                var boardManager = new BoardManager();
                int steps = 2;

                boardManager.CalculateStateXStepsAway(board, steps);

                var expectedState = @"[[0,0,0],[0,0,1],[0,1,1]]";

                Assert.Equal(board.State, expectedState);
            }
        }

        public class TheCalculateFinalStateMethod : BoardManagerTest
        {
            [Fact]
            public void ShouldStabilizeAndReturnFinalStateBeforeMaxSteps()
            {
                string initialState = @"[[1,1,0],[1,1,0],[0,0,0]]"; //This config keeps the board stable
                var board = new Board(initialState);

                var boardManager = new BoardManager();
                int maxSteps = 10;

                boardManager.CalculateFinalState(board, maxSteps);

                var expectedState = @"[[1,1,0],[1,1,0],[0,0,0]]";
                Assert.Equal(board.State, expectedState);
            }

            [Fact]
            public void ShouldThrowExceptionIfBoardDoesNotStabilizeAfterMaxSteps()
            {
                var initialState = @"[[0,1,0],[0,1,0],[0,1,0]]"; //This config generates an oscilator
                var board = new Board(initialState);

                var boardManager = new BoardManager();
                int maxSteps = 5;

                var exception = Assert.Throws<InvalidOperationException>(() => boardManager.CalculateFinalState(board, maxSteps));
                Assert.Equal($"Board did not stabilize after {maxSteps} steps.", exception.Message);
            }
        }
    }
}
