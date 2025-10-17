using Libs.Core.Patterns.Memento;

namespace Features.Gameplay.Domain
{
    public readonly struct GameplayMemento : IMemento
    {
        public readonly uint[] BoardState;
        public readonly ushort CurrentShape;
        public readonly byte ShapePositionX;
        public readonly byte ShapePositionY;

        public GameplayMemento(uint[] boardState, ushort currentShape, byte shapePositionX, byte shapePositionY)
        {
            BoardState = boardState;
            CurrentShape = currentShape;
            ShapePositionX = shapePositionX;
            ShapePositionY = shapePositionY;
        }
    }
}