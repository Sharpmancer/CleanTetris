namespace Features.Playfield.Domain.Model
{
    internal interface IGravityCalculationStrategy
    {
        float GetFallRowDuration(int level);
    }
}