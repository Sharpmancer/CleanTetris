using System;
using Features.Gameplay.Domain;
using Libs.Core.Patterns.Snapshot;

namespace Features.Gameplay.App
{
    [Serializable]
    public struct GameplaySnapshot : ISnapshot<GameplayMemento>
    {
        public uint[] BoardState;
        public ushort CurrentShape;
        public byte ShapePositionX;
        public byte ShapePositionY;
        public ushort TotalRowsCleared;

        public GameplayMemento ToMemento() => 
            new(BoardState, CurrentShape, ShapePositionX, ShapePositionY, TotalRowsCleared);

        public ISnapshot<GameplayMemento> Hydrate(GameplayMemento memento)
        {
            BoardState = memento.BoardState;
            CurrentShape = memento.CurrentShape;
            ShapePositionX = (byte)memento.ShapePositionX;
            ShapePositionY = (byte)memento.ShapePositionY;
            TotalRowsCleared = (ushort)memento.TotalRowsCleared;
            return this;
        }
    }
}