using System;

namespace Libs.Core.Rng
{
    public class SystemRandomBasedRng : IRng
    {
        private static readonly Random _rng = new();
        
        public int RandomInt(int minInclusive, int maxExclusive) => 
            _rng.Next(minInclusive, maxExclusive);
    }
}