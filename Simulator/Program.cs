using ConwayGameOfLife.Business.Managers;
using Domain.Entities;

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
            var boardManager = new BoardManager();
            boardManager.CalculateNextGridState(_board);
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
        private static void Main(string[] args)
        {
            string stateJson = @"[
                [1, 1, 0, 0, 0, 0, 0, 1, 1, 1],
                [1, 1, 0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 1, 0, 0, 1, 0, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 1, 1, 0, 0],
                [0, 1, 0, 0, 0, 1, 0, 0, 1, 0],
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
                Thread.Sleep(100);
            }
        }
    }
}