using System;
using System.Numerics;
using System.Collections.Generic;

namespace Libs.Bitmasks
{
    /// <summary>
    /// Represents a compact 2D boolean grid using bitmasks.
    /// Each row is stored as a single 32-bit unsigned integer.
    /// </summary>
    public class BitMask2D : IReadOnlyBitMask2D
    {
        private readonly uint[] _rows;
        public int Columns { get; }
        public int Rows { get; }

        public BitMask2D(int columns, int rows)
        {
            if (columns is <= 0 or > 32)
                throw new ArgumentOutOfRangeException(nameof(columns), "Columns must be between 1 and 32.");
            if (rows <= 0)
                throw new ArgumentOutOfRangeException(nameof(rows));

            Columns = columns;
            Rows = rows;
            _rows = new uint[rows];
        }

        public bool IsSet(int x, int y)
        {
            ValidateCoords(x, y);
            return (_rows[y] & (1u << x)) != 0;
        }

        public void Set(int x, int y)
        {
            ValidateCoords(x, y);
            _rows[y] |= 1u << x;
        }

        public void Set(IEnumerable<(int x, int y)> points)
        {
            foreach (var (cellX, cellY) in points)
                Set(cellX, cellY);
        }

        public void ClearCell(int x, int y)
        {
            ValidateCoords(x, y);
            _rows[y] &= ~(1u << x);
        }

        public void Clear()
        {
            for (var i = 0; i < _rows.Length; i++) 
                _rows[i] = 0;
        }

        public void ClearRow(int y)
        {
            ValidateRow(y);
            _rows[y] = 0;
        }

        public bool RowIsFull(int y)
        {
            ValidateRow(y);
            return _rows[y] == (1u << Columns) - 1;
        }

        public bool RowIsEmpty(int y)
        {
            ValidateRow(y);
            return _rows[y] == 0;
        }

        public void SwapRows(int y1, int y2)
        {
            ValidateRow(y1);
            ValidateRow(y2);
            (_rows[y1], _rows[y2]) = (_rows[y2], _rows[y1]);
        }

        public bool OutOfBounds(int x, int y) =>
            (uint)x >= (uint)Columns || (uint)y >= (uint)Rows;

        public void WriteTo(BitMask2D mask)
        {
            if(mask is null || mask.Columns != Columns || mask.Rows != Rows)
                throw new ArgumentException("Invalid mask argument.");

            for (var i = 0; i < mask._rows.Length; i++) 
                mask._rows[i] = _rows[i];
        }

        public void SetValue(uint[] value) => 
            Array.Copy(value, 0, _rows, 0, Rows);

        public IEnumerable<(int x, int y)> AllSetCells()
        {
            for (var y = 0; y < Rows; y++)
            {
                var row = _rows[y];
                while (row != 0)
                {
                    var x = row.TrailingZeroCount();
                    if (x < Columns)
                        yield return (x, y);
                    row &= row - 1; // clear lowest set bit
                }
            }
        }

        public uint[] CloneUnderlyingValue() => 
            _rows.Clone() as uint[];

        private void ValidateCoords(int x, int y)
        {
            if (OutOfBounds(x, y))
                throw new ArgumentOutOfRangeException($"({x}, {y}) is out of bounds.");
        }

        private void ValidateRow(int y)
        {
            if ((uint)y >= (uint)Rows)
                throw new ArgumentOutOfRangeException(nameof(y));
        }
    }
}