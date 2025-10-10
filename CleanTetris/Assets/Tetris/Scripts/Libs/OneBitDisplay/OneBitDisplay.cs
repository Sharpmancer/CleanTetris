using System.Collections.Generic;
using UnityEngine;

namespace Libs.OneBitDisplay
{
    public abstract class OneBitDisplay : MonoBehaviour, IOneBitDisplay
    {
        public abstract void Initialize(int x, int y);
        public abstract void SetPixels(IEnumerable<(int x, int y)> activePixels);
    }
}