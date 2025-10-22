using System;

namespace Features.Score.Domain.Model
{
    internal class NesPointsPerRowsClearedCalculationStrategy : IPointsPerRowsClearedCalculationStrategy
    {
        public int GetPoints(int rowsCleared) =>
            rowsCleared switch
            {
                1 => 40,
                2 => 100,
                3 => 300,
                4 => 1200,
                _ => throw new ArgumentOutOfRangeException(nameof(rowsCleared), rowsCleared, null)
            };
    }
}