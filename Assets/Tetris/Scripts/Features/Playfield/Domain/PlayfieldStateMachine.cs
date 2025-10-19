using System;
using System.Linq;
using Features.Playfield.Domain.States;
using Libs.Core.Lifecycle;

namespace Features.Playfield.Domain
{
    internal class PlayfieldStateMachine : IDisposable
    {
        private readonly PlayfieldState[] _states = {
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
        
        private PlayfieldState _currentState;
        
        internal void PlayfieldState<T>(Playfield playfield) where T : PlayfieldState => 
            (_currentState = _states.First(n => n is T)).Enter(playfield);
        
        internal void Tick(float timeDelta) => 
            (_currentState as ITickable)?.Tick(timeDelta);

        public void Dispose()
        {
            foreach (var state in _states) 
                (state as IDisposable)?.Dispose();
        }
    }
}