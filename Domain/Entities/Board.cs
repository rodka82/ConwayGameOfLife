using System.Text.Json;

namespace Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public int[,] Grid { get; set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public string State { get; set; }

        public Board()
        {
                
        }
        
        public Board(string initialState)
        {
            SetupBoard(initialState);
        }

        public void SetupBoard()
        {
            SetupBoard(State);
        }

        public void SetupBoard(string serializedInitialState)
        {
            State = serializedInitialState ?? throw new ArgumentNullException(nameof(serializedInitialState));
            Grid = GetGrid();
            Rows = Grid.GetLength(0);
            Cols = Grid.GetLength(1);
        }

        public int[,] GetGrid()
        {
            int[][]? jaggedArray = JsonSerializer.Deserialize<int[][]>(State);

            if (jaggedArray == null || jaggedArray.Length == 0 || jaggedArray[0] == null)
            {
                throw new InvalidOperationException("Invalid board state: The grid cannot be null or empty.");
            }

            int rows = jaggedArray.Length;
            int cols = jaggedArray[0].Length;
            int[,] grid = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = jaggedArray[i][j];
                }
            }

            return grid;
        }
    }
}