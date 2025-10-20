namespace Features.Playfield.Domain
{
    public interface IGravityCalculationStrategy
    {
        float GetFallRowDuration(int level);
    }
}