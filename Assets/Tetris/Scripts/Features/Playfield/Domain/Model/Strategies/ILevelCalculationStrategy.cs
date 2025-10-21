namespace Features.Playfield.Domain.Model
{
    internal interface ILevelCalculationStrategy
    {
        int GetLevel(int totalRowsCleared);
    }
}