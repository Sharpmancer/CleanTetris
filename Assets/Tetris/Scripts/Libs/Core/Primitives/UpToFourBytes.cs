using System;

namespace Libs.Core.Primitives
{
    /// <summary>
    /// Compact container for up to 4 byte-sized values (e.g., a few indices 0–255).
    /// Internally stored as a single uint. Accessed by index 0..3.
    /// </summary>
    public struct UpToFourBytes
    {
        private uint _data;
        public int Count { get; private set; }

        public void Add(byte value)
        {
            if (Count >= 4)
                throw new InvalidOperationException();

            _data |= (uint)value << (Count * 8);
            Count++;
        }

        public byte this[int index]
        {
            get
            {
                if ((uint)index >= Count || index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return (byte)((_data >> (index * 8)) & 0xFF);
            }
        }
    }
}