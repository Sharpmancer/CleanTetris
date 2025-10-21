using System;
using System.Collections.Generic;

namespace Libs.Core.DotNetUtils
{
    public static class ListExtensions
    {
        private static readonly Random _rng = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = _rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}