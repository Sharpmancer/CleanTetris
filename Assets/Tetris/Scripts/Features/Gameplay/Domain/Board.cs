using System;
using Libs.Bitmasks;

namespace Features.Gameplay.Domain
{
    internal sealed class Board
    {
        private readonly BitMask2D _mask;

        internal int Columns => _mask.Columns;
        internal int Rows => _mask.Rows;
        internal IReadOnlyBitMask2D Mask => _mask;

        internal Board(int columns, int rows) => 
            _mask = new BitMask2D(columns, rows);

        internal void PlaceShape(Shape shape, GridCoordinates coordinates, bool skipFitCheck = false)
        {
            if (!skipFitCheck && !CanFit(shape, coordinates))
                throw new InvalidOperationException("Shape does not fit at the given coordinates.");

            foreach (var (cellX, cellY) in shape.Cells()) 
                _mask.Set(coordinates.Column + cellX, coordinates.Row + cellY);
        }

        internal void RemoveShape(Shape shape, GridCoordinates coordinates)
        {
            foreach (var (cellX, cellY) in shape.Cells()) 
                _mask.ClearCell(coordinates.Column + cellX, coordinates.Row + cellY);
        }

        internal bool CanFit(Shape shape, GridCoordinates coordinates)
        {
            foreach (var (cellX, cellY) in shape.Cells())
            {
                var x = coordinates.Column + cellX;
                var y = coordinates.Row + cellY;

                if (_mask.OutOfBounds(x, y))
                    return false;

                if (_mask.IsSet(x, y))
                    return false;
            }
            return true;
        }

        internal bool RowIsFull(int y) => 
            _mask.RowIsFull(y);
        
        internal bool RowIsEmpty(int y) => 
            _mask.RowIsEmpty(y);
        
        internal void ClearRow(int y) => 
            _mask.ClearRow(y);
        
        internal void SwapRows(int y1, int y2) => 
            _mask.SwapRows(y1, y2);
        
        internal void SetValue(uint[] value) =>
            _mask.SetValue(value);
    }
}