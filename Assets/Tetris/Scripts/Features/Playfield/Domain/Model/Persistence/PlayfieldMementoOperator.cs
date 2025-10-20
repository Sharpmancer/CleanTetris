namespace Features.Playfield.Domain
{
    internal class PlayfieldMementoOperator
    {
        private readonly Playfield _playfield;

        internal PlayfieldMementoOperator(Playfield playfield) => 
            _playfield = playfield;

        internal PlayfieldMemento GetMemento() =>
            new(_playfield.BoardState.CloneUnderlyingValue(), 
                _playfield.CurrentShape?.Mask ?? 0,
                _playfield.ShapePosition.Column,
                _playfield.ShapePosition.Row,
                _playfield.TotalRowsCleared);

        internal void SetMemento(PlayfieldMemento memento)
        {
            _playfield.Board.SetValue(memento.BoardState);
            _playfield.TotalRowsCleared = memento.TotalRowsCleared;
            _playfield.RecalculateGravity();
            
            if(memento.CurrentShape == 0)
                return;
            _playfield.CurrentShape = new Shape(memento.CurrentShape);
            _playfield.ShapePosition = new GridCoordinates(column: memento.ShapePositionX, row: memento.ShapePositionY);
            _playfield.HandleBoardStateChanged();
        }
    }
}