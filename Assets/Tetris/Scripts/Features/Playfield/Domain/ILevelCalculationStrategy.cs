namespace Features.Playfield.Domain
{
    public interface ILevelCalculationStrategy
    {
        int GetLevel(int totalRowsCleared);
    }
}