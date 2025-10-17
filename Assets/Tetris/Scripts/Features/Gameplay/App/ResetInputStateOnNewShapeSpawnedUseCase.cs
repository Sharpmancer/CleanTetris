using System;
using Features.Input.App;
using Libs.Core.Lifecycle;

namespace Features.Gameplay.App
{
    public class ResetInputStateOnNewShapeSpawnedUseCase : IInitializable, IDisposable
    {
        private readonly Domain.IGameplayEventsDispatcher _gameplayEvents;
        private readonly IInputStateResetter _inputStateResetter;

        public ResetInputStateOnNewShapeSpawnedUseCase(Domain.IGameplayEventsDispatcher gameplayEvents, IInputStateResetter inputStateResetter)
        {
            _gameplayEvents = gameplayEvents;
            _inputStateResetter = inputStateResetter;
        }

        public void Initialize() => 
            _gameplayEvents.OnNewShapeSpawned += _inputStateResetter.Reset;

        public void Dispose() => 
            _gameplayEvents.OnNewShapeSpawned -= _inputStateResetter.Reset;
    }
}