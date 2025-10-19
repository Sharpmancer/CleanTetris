using System;
using Features.Input.App;
using Libs.Core.Lifecycle;

namespace Features.Playfield.App
{
    public class ResetInputStateOnNewShapeSpawnedUseCase : IInitializable, IDisposable
    {
        private readonly Domain.IPlayfieldEventsDispatcher _playfieldEvents;
        private readonly IInputStateResetter _inputStateResetter;

        public ResetInputStateOnNewShapeSpawnedUseCase(Domain.IPlayfieldEventsDispatcher playfieldEvents, IInputStateResetter inputStateResetter)
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