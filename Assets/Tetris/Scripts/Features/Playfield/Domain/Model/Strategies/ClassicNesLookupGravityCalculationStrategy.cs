using System;

namespace Features.Playfield.Domain
{
    internal class ClassicNesLookupGravityCalculationStrategy : IGravityCalculationStrategy
    {
        private const int NES_FRAMERATE = 60;

        public float GetFallRowDuration(int level) => 
            (float)GetFramesPerRow(level) / NES_FRAMERATE;

        /// <summary>Frames per row at the given NES CLASSIC level (exact lookup; 60 FPS).</summary>
        private static int GetFramesPerRow(int level)
        {
            if (level < 0) level = 0;

            return level switch
            {
                0 => 48,
                1 => 43,
                2 => 38,
                3 => 33,
                4 => 28,
                5 => 23,
                6 => 18,
                7 => 13,
                8 => 8,
                9 => 6,
                >= 10 and <= 12 => 5,
                >= 13 and <= 15 => 4,
                >= 16 and <= 18 => 3,
                >= 19 and <= 28 => 2,
                > 28 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(level), level, null) 
            };
        }
    }
}