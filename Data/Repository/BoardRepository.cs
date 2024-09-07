using ConwayGameOfLife.Infra.Data.Context;
using ConwayGameOfLife.Infra.Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife.Infra.Data.Repository
{
    public class BoardRepository : IBoardRepository
    {
        protected readonly GameOfLifeDbContext Context;
        protected readonly DbSet<Board> Boards;

        public BoardRepository(GameOfLifeDbContext context)
        {
            Context = context;
            Boards = Context.Set<Board>();
        }

        public async Task<int> AddBoardAsync(Board board)
        {
            await Boards.AddAsync(board);
            return await Context.SaveChangesAsync(); 
        }

        public async Task<Board> GetBoardByIdAsync(int boardId)
        {
            var board = await Boards.FirstOrDefaultAsync(x => x.Id == boardId);
            if (board == null)
                throw new Exception($"Board with ID {boardId} not found.");

            return board;
        }

        public async Task UpdateBoardAsync(Board board)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            Boards.Attach(board); 
            Context.Entry(board).State = EntityState.Modified; 
            await Context.SaveChangesAsync();
        }
    }
}
