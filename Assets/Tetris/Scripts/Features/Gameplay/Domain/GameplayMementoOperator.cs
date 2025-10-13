namespace Features.Gameplay.Domain
{
    public class GameplayMementoOperator : IGameplayMementoProvider, IGameplayMementoConsumer
    {
        private readonly GameplayMediator _gameplayMediator;

        public GameplayMementoOperator(GameplayMediator gameplayMediator) => 
            _gameplayMediator = gameplayMediator;

        public Memento GetMemento() =>
            new(_gameplayMediator.BoardState.CloneUnderlyingValue(), 
                _gameplayMediator.CurrentShape?.Mask ?? 0,
                (byte)_gameplayMediator.ShapePosition.Column,
                (byte)_gameplayMediator.ShapePosition.Row);

        public void SetMemento(Memento memento)
        {
            _gameplayMediator.Board.SetValue(memento.BoardState);
            if(memento.CurrentShape == 0)
                return;
            _gameplayMediator.CurrentShape = new Shape(memento.CurrentShape);
            _gameplayMediator.ShapePosition = new GridCoordinates(column: memento.ShapePositionX, row: memento.ShapePositionY);
            _gameplayMediator.HandleBoardStateChanged();
        }
    }
}