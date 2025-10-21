namespace Features.Playfield.Domain
{
    internal interface ILevelCalculationStrategy
    {
        int GetLevel(int totalRowsCleared);
    }
}