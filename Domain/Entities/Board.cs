using System.Text.Json;

namespace Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public int[,] Grid { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public string State { get; set; }

        public Board()
        {
                
        }
        
        public Board(string initialState)
        {
            State = initialState;
        }
    }
}