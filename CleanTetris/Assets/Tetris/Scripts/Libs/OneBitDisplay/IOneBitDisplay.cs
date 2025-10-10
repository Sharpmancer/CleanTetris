using System.Collections.Generic;

namespace Libs.OneBitDisplay
{
    public interface IOneBitDisplay
    {
        void Initialize(int x, int y);
        void SetPixels(IEnumerable<(int x, int y)> activePixels);
    }
}