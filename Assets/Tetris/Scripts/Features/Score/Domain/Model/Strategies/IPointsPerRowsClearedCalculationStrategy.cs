namespace Features.Score.Domain.Model
{
    internal interface IPointsPerRowsClearedCalculationStrategy
    {
        int GetPoints(int rowsCleared);
    }
}