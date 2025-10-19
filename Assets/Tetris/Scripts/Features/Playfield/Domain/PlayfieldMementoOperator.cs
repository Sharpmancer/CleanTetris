using Libs.Core.Patterns.Memento;

namespace Features.Playfield.Domain
{
    // TODO: internalize usage via domain aggregate
    public class PlayfieldMementoOperator : IMementoProvider<GameplayMemento>, IMementoConsumer<GameplayMemento>
    {
        private readonly Playfield _playfield;

        public PlayfieldMementoOperator(Playfield playfield) => 
            _playfield = playfield;

        public GameplayMemento GetMemento() =>
            new(_playfield.BoardState.CloneUnderlyingValue(), 
                _playfield.CurrentShape?.Mask ?? 0,
                _playfield.ShapePosition.Column,
                _playfield.ShapePosition.Row,
                _playfield.TotalRowsCleared);

        public void SetMemento(GameplayMemento memento)
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