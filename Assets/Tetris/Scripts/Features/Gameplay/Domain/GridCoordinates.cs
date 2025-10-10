namespace Features.Gameplay.Domain
{
    internal readonly struct GridCoordinates
    {
        internal int Column { get; }
        internal int Row { get; }
        
        public static GridCoordinates operator +(GridCoordinates a, GridCoordinates b) =>
            new(column: a.Column + b.Column, row: a.Row + b.Row);

        public static GridCoordinates operator -(GridCoordinates a, GridCoordinates b) =>
            new(column: a.Column - b.Column, row: a.Row - b.Row);
        
        internal GridCoordinates(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public override string ToString() => 
            $"({Column}, {Row})";
    }
}