namespace Features.Gameplay.Domain
{
    public interface ILevelCalculationStrategy
    {
        int GetLevel(int totalRowsCleared);
    }
}