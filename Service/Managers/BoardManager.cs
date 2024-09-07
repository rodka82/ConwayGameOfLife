using ConwayGameOfLife.Business.Services.Interfaces;
using Domain.Entities;
using System.Text.Json;

namespace ConwayGameOfLife.Business.Managers
{
    public class BoardManager : IBoardManager
    {
        public BoardManager() { }
        private const int DEAD_CELL = 0;
        private const int LIVE_CELL = 1;

        public void CalculateNextGridState(Board board)
        {
            int[,] newGrid = new int[board.Rows, board.Cols];

            for (int currentRow = 0; currentRow < board.Rows; currentRow++)
            {
                for (int currentCol = 0; currentCol < board.Cols; currentCol++)
                {
                    int numOfAliveNeighbors = GetNeighbors(board, currentRow, currentCol);
                    bool isAlive = IsCellAlive(board.Grid, currentRow, currentCol);

                    if (isAlive)
                        SetLiveCell(newGrid, currentRow, currentCol, numOfAliveNeighbors);
                    else
                        SetDeadCell(newGrid, currentRow, currentCol, numOfAliveNeighbors);
                }
            }

            string newState = SerializeGrid(newGrid);
            board.SetupBoard(newState);

        }

        public void CalculateStateXStepsAway(Board board, int xSteps)
        {
            for(var i = 0; i < xSteps; i++)
                CalculateNextGridState(board);
        }
        
        public void CalculateFinalState(Board board, int maxSteps)
        {
            int[,] previousGrid = null;
            for (var i = 0; i < maxSteps; i++)
            {
                var currentGrid = board.GetGrid();

                if (previousGrid != null && GridsAreEqual(previousGrid, currentGrid))
                {
                    var serializedState = SerializeGrid(currentGrid);
                    board.SetupBoard(serializedState); 
                    return; // Grid is Stable (concluded)
                }

                previousGrid = currentGrid;

                CalculateNextGridState(board);
            }

            throw new InvalidOperationException($"Board did not stabilize after {maxSteps} steps.");
        }

        private string SerializeGrid(int[,] grid)
        {
            var jaggedArray = ConvertToJaggedArray(grid);
            return JsonSerializer.Serialize(jaggedArray);
        }

        private int[][] ConvertToJaggedArray(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int[][] jaggedArray = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = grid[i, j];
                }
            }

            return jaggedArray;
        }

        private bool GridsAreEqual(int[,] grid1, int[,] grid2)
        {
            if (grid1.GetLength(0) != grid2.GetLength(0) || grid1.GetLength(1) != grid2.GetLength(1))
            return false;

            for (int i = 0; i < grid1.GetLength(0); i++)
            {
                for (int j = 0; j < grid1.GetLength(1); j++)
                {
                    if (grid1[i, j] != grid2[i, j])
                        return false;
                }
            }

            return true; 
        }

        public int GetNeighbors(Board board, int row, int col)
        {
            int liveNeighborsCells = 0;

            for (int currentRow = -1; currentRow <= 1; currentRow++)
            {
                for (int currentCol = -1; currentCol <= 1; currentCol++)
                {
                    if (currentRow == 0 && currentCol == 0) continue;

                    int neighborRow = row + currentRow;
                    int neighborCol = col + currentCol;

                    if (IsNeighborWithinBoundsAndAlive(board, neighborRow, neighborCol))
                        liveNeighborsCells++;
                }
            }

            return liveNeighborsCells;
        }

        private static void SetDeadCell(int[,] newState, int i, int j, int numOfAliveNeighbors)
        {
            newState[i, j] = numOfAliveNeighbors == 3 ? LIVE_CELL : DEAD_CELL;
        }

        private static void SetLiveCell(int[,] newState, int i, int j, int numOfAliveNeighbors)
        {
            newState[i, j] = (numOfAliveNeighbors == 2 || numOfAliveNeighbors == 3) ? LIVE_CELL : DEAD_CELL;
        }

        private bool IsNeighborWithinBoundsAndAlive(Board board, int neighborRow, int neighborCol)
        {
            return IsNeighborRowWithinBounds(neighborRow, board.Rows) &&
                   IsNeighborColWithinBounds(neighborCol, board.Cols) &&
                   IsCellAlive(board.Grid, neighborRow, neighborCol);
        }

        private static bool IsNeighborRowWithinBounds(int neighborRow, int boardRows)
        {
            return neighborRow >= 0 && neighborRow < boardRows;
        }

        private static bool IsNeighborColWithinBounds(int neighborCol, int boardCols)
        {
            return neighborCol >= 0 && neighborCol < boardCols;
        }

        private static bool IsCellAlive(int[,] grid, int neighborRow, int neighborCol)
        {
            if (neighborRow >= 0 && neighborRow < grid.GetLength(0) &&
                neighborCol >= 0 && neighborCol < grid.GetLength(1))
            {
                return grid[neighborRow, neighborCol] == 1;
            }

            return false;
        }
    }
}
