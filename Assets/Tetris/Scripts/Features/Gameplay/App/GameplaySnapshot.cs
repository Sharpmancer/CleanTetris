using System;
using Features.Gameplay.Domain;

namespace Features.Gameplay.App
{
    [Serializable]
    public record GameplaySnapshot
    {
        public uint[] BoardState;
        public ushort CurrentShape;
        public byte ShapePositionX;
        public byte ShapePositionY;

        internal GameplayMemento ToMemento() => 
            new(BoardState, CurrentShape, ShapePositionX, ShapePositionY);

        internal static GameplaySnapshot FromMemento(GameplayMemento gameplayMemento) =>
            new()
            {
                BoardState = gameplayMemento.BoardState,
                CurrentShape = gameplayMemento.CurrentShape,
                ShapePositionX = gameplayMemento.ShapePositionX,
                ShapePositionY = gameplayMemento.ShapePositionY,
            };
    }
}