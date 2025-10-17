using System;
using Features.Gameplay.Domain;
using Features.Input.App;
using Libs.Core.Lifecycle;

namespace Features.Gameplay.App
{
    public class ResetInputStateOnNewShapeSpawnedUseCase : IInitializable, IDisposable
    {
        private readonly IGameplayEvents _gameplayEvents;
        private readonly IInputStateResetter _inputStateResetter;

        public ResetInputStateOnNewShapeSpawnedUseCase(IGameplayEvents gameplayEvents, IInputStateResetter inputStateResetter)
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