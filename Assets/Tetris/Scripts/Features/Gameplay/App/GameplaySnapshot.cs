using Features.Gameplay.Domain;

namespace Features.Gameplay.App
{
    public record GameplaySnapshot
    {
        public uint[] BoardState;
        public ushort CurrentShape;
        public byte ShapePositionX;
        public byte ShapePositionY;

        internal Memento ToMemento() => 
            new(BoardState, CurrentShape, ShapePositionX, ShapePositionY);

        internal static GameplaySnapshot FromMemento(Memento memento) =>
            new()
            {
                BoardState = memento.BoardState,
                CurrentShape = memento.CurrentShape,
                ShapePositionX = memento.ShapePositionX,
                ShapePositionY = memento.ShapePositionY,
            };
    }
}