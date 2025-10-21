namespace Features.Playfield.Domain
{
    internal class OneLevelPerTenRowsClearedCalculationStrategy : ILevelCalculationStrategy
    {
        public int GetLevel(int totalRowsCleared) => 
            totalRowsCleared / 10;
    }
}