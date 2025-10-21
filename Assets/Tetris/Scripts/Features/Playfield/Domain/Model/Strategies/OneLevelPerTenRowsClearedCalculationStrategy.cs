namespace Features.Playfield.Domain.Model
{
    internal class OneLevelPerTenRowsClearedCalculationStrategy : ILevelCalculationStrategy
    {
        public int GetLevel(int totalRowsCleared) => 
            totalRowsCleared / 10;
    }
}