using Libs.Core.Patterns.Memento;

namespace Features.Gameplay.Domain
{
    public class GameplayMementoOperator : IMementoProvider<GameplayMemento>, IMementoConsumer<GameplayMemento>
    {
        private readonly GameplayMediator _gameplayMediator;

        public GameplayMementoOperator(GameplayMediator gameplayMediator) => 
            _gameplayMediator = gameplayMediator;

        public GameplayMemento GetMemento() =>
            new(_gameplayMediator.BoardState.CloneUnderlyingValue(), 
                _gameplayMediator.CurrentShape?.Mask ?? 0,
                _gameplayMediator.ShapePosition.Column,
                _gameplayMediator.ShapePosition.Row,
                _gameplayMediator.TotalRowsCleared);

        public void SetMemento(GameplayMemento memento)
        {
            _gameplayMediator.Board.SetValue(memento.BoardState);
            _gameplayMediator.TotalRowsCleared = memento.TotalRowsCleared;
            _gameplayMediator.RecalculateGravity();
            
            if(memento.CurrentShape == 0)
                return;
            _gameplayMediator.CurrentShape = new Shape(memento.CurrentShape);
            _gameplayMediator.ShapePosition = new GridCoordinates(column: memento.ShapePositionX, row: memento.ShapePositionY);
            _gameplayMediator.HandleBoardStateChanged();
        }
    }
}