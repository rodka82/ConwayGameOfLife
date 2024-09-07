using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife.Business.Services.Interfaces
{
    public interface IBoardManager
    {
        void CalculateNextGridState(Board board);
        void CalculateStateXStepsAway(Board board, int x);
        void CalculateFinalState(Board board, int maxSteps);
    }
}
