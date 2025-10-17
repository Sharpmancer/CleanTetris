namespace Features.Gameplay.Domain
{
    public class OneLevelPerTenRowsClearedCalculationStrategy : ILevelCalculationStrategy
    {
        public int GetLevel(int totalRowsCleared) => 
            totalRowsCleared / 10;
    }
}