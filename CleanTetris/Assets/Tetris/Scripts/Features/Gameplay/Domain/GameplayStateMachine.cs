using System;
using System.Linq;
using Features.Gameplay.Domain.States;
using Libs.Core;

namespace Features.Gameplay.Domain
{
    internal class GameplayStateMachine : IDisposable
    {
        private readonly GameplayState[] _gameplayStates = {
            new StartGameState(),
            new IdleState(),
            new SpawnNewShapeState(),
            new MoveShapeState(),
            new RotateShapeState(),
            new LockShapeState(),
            new TryClearRowsState(),
            new CompactBoardState(),
            new GameOverState()
        };
        
        private GameplayState _currentState;
        
        internal void ChangeState<T>(GameplayMediator mediator) where T : GameplayState => 
            (_currentState = _gameplayStates.First(n => n is T)).Enter(mediator);
        
        internal void Tick(float timeDelta) => 
            (_currentState as ITickable)?.Tick(timeDelta);

        public void Dispose()
        {
            foreach (var gameplayState in _gameplayStates) 
                (gameplayState as IDisposable)?.Dispose();
        }
    }
}