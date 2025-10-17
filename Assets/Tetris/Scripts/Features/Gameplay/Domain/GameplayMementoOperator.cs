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
                (byte)_gameplayMediator.ShapePosition.Column,
                (byte)_gameplayMediator.ShapePosition.Row);

        public void SetMemento(GameplayMemento gameplayMemento)
        {
            _gameplayMediator.Board.SetValue(gameplayMemento.BoardState);
            if(gameplayMemento.CurrentShape == 0)
                return;
            _gameplayMediator.CurrentShape = new Shape(gameplayMemento.CurrentShape);
            _gameplayMediator.ShapePosition = new GridCoordinates(column: gameplayMemento.ShapePositionX, row: gameplayMemento.ShapePositionY);
            _gameplayMediator.HandleBoardStateChanged();
        }
    }
}