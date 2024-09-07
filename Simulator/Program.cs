using ConwayGameOfLife.Business.Managers;
using Domain.Entities;
using System.Text;

namespace GameOfLife
{

    public class LifeSimulation
    {
        private Board _board;

        public LifeSimulation(Board board)
        {
            _board = board;
        }

        public void Simulate()
        {
            PrintBoard();
            Start();
        }


        private void Start()
        {
            var boardManager = new BoardManager();
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    int numOfAliveNeighbors = boardManager.GetNeighbors(_board, i, j);

                    if (_board.Grid[i, j] == 1)
                    {
                        if (numOfAliveNeighbors < 2)
                        {
                            _board.Grid[i, j] = 0;
                        }

                        if (numOfAliveNeighbors > 3)
                        {
                            _board.Grid[i, j] = 0;
                        }
                    }
                    else
                    {
                        if (numOfAliveNeighbors == 3)
                        {
                            _board.Grid[i, j] = 1;
                        }
                    }
                }
            }
        }

        private void PrintBoard()
        {
            for (int i = 0; i < _board.Rows; i++)
            {
                for (int j = 0; j < _board.Cols; j++)
                {
                    Console.Write(_board.Grid[i, j] == 1 ? "#" : " ");
                    if (j == _board.Cols - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }
    }

    

    internal class Program
    {
        private static string ConvertHashSetToGridString(HashSet<(int, int)> boardState, int rows, int cols)
        {
            int[,] grid = new int[rows, cols];

            // Populate the grid based on the live cells in the HashSet
            foreach (var (row, col) in boardState)
            {
                grid[row, col] = 1;
            }

            // Convert the grid to a string in the format of int[,]
            StringBuilder gridStringBuilder = new StringBuilder();
            gridStringBuilder.Append("new int[,] { ");

            for (int i = 0; i < rows; i++)
            {
                gridStringBuilder.Append("{ ");
                for (int j = 0; j < cols; j++)
                {
                    gridStringBuilder.Append(grid[i, j]);

                    // Add a comma unless it's the last element in the row
                    if (j < cols - 1)
                    {
                        gridStringBuilder.Append(", ");
                    }
                }
                gridStringBuilder.Append(" }");

                // Add a comma unless it's the last row
                if (i < rows - 1)
                {
                    gridStringBuilder.Append(", ");
                }
            }

            gridStringBuilder.Append(" }");

            return gridStringBuilder.ToString();
        }
        private static void Main(string[] args)
        {
            // Assuming State contains the grid in JSON format like the one above
            string stateJson = @"[
                [1, 1, 0, 0, 0, 0, 0, 1, 1, 1],
                [1, 1, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 1, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0, 1, 0],
                [0, 1, 1, 1, 1, 0, 0, 0, 1, 0],
                [0, 0, 1, 0, 1, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 1, 1, 0, 1, 0, 1],
                [0, 0, 0, 0, 0, 0, 1, 0, 0, 0],
                [0, 1, 0, 0, 0, 1, 1, 1, 1, 0]
            ]";

            Board board = new Board(stateJson);

            int MaxRuns = 100;
            int runs = 0;
            LifeSimulation sim = new LifeSimulation(board);

            while (runs++ < MaxRuns)
            {
                sim.Simulate();
                System.Threading.Thread.Sleep(1500);
            }
        }
    }
}