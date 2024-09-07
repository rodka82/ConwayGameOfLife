using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife.Infra.Data.Interfaces
{
    public interface IBoardRepository
    {
        Task<int> AddBoardAsync(Board board);
        Task<Board> GetBoardByIdAsync(int boardId);
        Task UpdateBoardAsync(Board board);
    }
}
