using System.Collections.Generic;

namespace Libs.Bitmasks
{
    public interface IReadOnlyBitMask2D
    {
        int Columns { get; }
        int Rows { get; }
        bool IsSet(int x, int y);
        bool RowIsFull(int y);
        bool RowIsEmpty(int y);
        bool OutOfBounds(int x, int y);
        void WriteTo(BitMask2D mask);
        IEnumerable<(int x, int y)> AllSetCells();
        uint[] CloneUnderlyingValue();
    }
}