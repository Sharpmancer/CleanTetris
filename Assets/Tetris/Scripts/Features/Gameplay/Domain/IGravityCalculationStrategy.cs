namespace Features.Gameplay.Domain
{
    public interface IGravityCalculationStrategy
    {
        float GetFallRowDuration(int level);
    }
}