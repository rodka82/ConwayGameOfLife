using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife.Business.Interfaces
{
    public interface IBoardService
    {
        Task<int> AddBoardAsync(string initialState);
        Task<string> CalculateNextStateAsync(int boardId);
        Task<string> GetStateXStepsAwayAsync(int boardId, int xSteps);
        Task<string> CalculateFinalStateAsync(int boardId, int maxSteps);
    }
}
