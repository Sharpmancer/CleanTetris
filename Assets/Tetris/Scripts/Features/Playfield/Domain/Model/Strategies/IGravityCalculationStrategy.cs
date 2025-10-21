namespace Features.Playfield.Domain
{
    internal interface IGravityCalculationStrategy
    {
        float GetFallRowDuration(int level);
    }
}