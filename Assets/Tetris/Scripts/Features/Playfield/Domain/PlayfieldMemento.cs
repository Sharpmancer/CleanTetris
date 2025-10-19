using Libs.Core.Patterns.Memento;

namespace Features.Playfield.Domain
{
    public readonly struct GameplayMemento : IMemento
    {
        public readonly uint[] BoardState;
        public readonly ushort CurrentShape;
        public readonly int ShapePositionX;
        public readonly int ShapePositionY;
        public readonly int TotalRowsCleared;

        public GameplayMemento(uint[] boardState, ushort currentShape, int shapePositionX, int shapePositionY, int totalRowsCleared)
        {
            BoardState = boardState;
            CurrentShape = currentShape;
            ShapePositionX = shapePositionX;
            ShapePositionY = shapePositionY;
            TotalRowsCleared = totalRowsCleared;
        }
    }
}