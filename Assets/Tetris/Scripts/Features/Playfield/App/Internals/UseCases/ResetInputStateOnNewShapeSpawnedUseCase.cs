using System;
using Features.Input.App;
using Features.Input.App.Api;
using Libs.Core.Lifecycle;

namespace Features.Playfield.App
{
    internal class ResetInputStateOnNewShapeSpawnedUseCase : IInitializable, IDisposable
    {
        private readonly Domain.Api.IPlayfieldEventsDispatcher _playfieldEvents;
        private readonly IInputStateResetter _inputStateResetter;

        internal ResetInputStateOnNewShapeSpawnedUseCase(Domain.Api.IPlayfieldEventsDispatcher playfieldEvents, IInputStateResetter inputStateResetter)
        {
            _playfieldEvents = playfieldEvents;
            _inputStateResetter = inputStateResetter;
        }

        public void Initialize() => 
            _playfieldEvents.OnNewShapeSpawned += _inputStateResetter.Reset;

        public void Dispose() => 
            _playfieldEvents.OnNewShapeSpawned -= _inputStateResetter.Reset;
    }
}