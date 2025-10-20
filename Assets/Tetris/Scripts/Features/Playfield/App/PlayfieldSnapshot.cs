using System;
using Features.Playfield.Domain;
using Libs.Core.Patterns.Snapshot;

namespace Features.Playfield.App
{
    [Serializable]
    public struct PlayfieldSnapshot : ISnapshot<PlayfieldMemento>
    {
        public uint[] BoardState;
        public ushort CurrentShape;
        public byte ShapePositionX;
        public byte ShapePositionY;
        public ushort TotalRowsCleared;

        public PlayfieldMemento ToMemento() => 
            new(BoardState, CurrentShape, ShapePositionX, ShapePositionY, TotalRowsCleared);

        public ISnapshot<PlayfieldMemento> Hydrate(PlayfieldMemento memento)
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