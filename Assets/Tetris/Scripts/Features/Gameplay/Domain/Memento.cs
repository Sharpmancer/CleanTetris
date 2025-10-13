namespace Features.Gameplay.Domain
{
    public readonly struct Memento
    {
        public readonly uint[] BoardState;
        public readonly ushort CurrentShape;
        public readonly byte ShapePositionX;
        public readonly byte ShapePositionY;

        public Memento(uint[] boardState, ushort currentShape, byte shapePositionX, byte shapePositionY)
        {
            BoardState = boardState;
            CurrentShape = currentShape;
            ShapePositionX = shapePositionX;
            ShapePositionY = shapePositionY;
        }
    }
}