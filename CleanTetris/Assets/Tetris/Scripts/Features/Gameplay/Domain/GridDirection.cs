using System;

namespace Features.Gameplay.Domain
{
    internal readonly struct GridDirection
    {
        private readonly int _x;
        private readonly int _y;

        internal static GridDirection Down { get; } = new(0, -1);
        internal static GridDirection Up { get; } = new(0, 1);
        internal static GridDirection Left { get; } = new(-1, 0);
        internal static GridDirection Right { get; } = new(1, 0);
        internal static GridDirection None { get; } = new(0, 0);

        public static implicit operator GridDirection(GridCoordinates coordinates) =>
            coordinates switch
            {
                {Row: 0, Column: 0} => None,
                { Row:  >0, Column: 0 }  => Up,
                { Row: <0, Column: 0 }  => Down,
                { Row:  0, Column: >0 }  => Right,
                { Row:  0, Column: <0 } => Left,
                _ => throw new ArgumentException( $"Invalid direction {coordinates}: must be non-zero on either none or exactly one axis.")
            };

        public static bool operator ==(GridDirection left, GridDirection right) => left._x == right._x && left._y == right._y;
        public static bool operator !=(GridDirection left, GridDirection right) => !(left == right);
        public static implicit operator GridCoordinates(GridDirection direction) => new(column: direction._x, row: direction._y);

        private GridDirection(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            var directionText = this == Down 
                ? "Down" 
                : this == Up 
                    ? "Up" 
                    : this == Left 
                        ? "Left" 
                        : this == Right 
                            ? "Right" 
                            : "None";
            return $"{directionText} ({_x}, {_y})";
        }
    }
}