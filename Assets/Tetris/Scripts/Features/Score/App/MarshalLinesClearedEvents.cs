using System;
using Features.Gameplay.App;
using Features.Score.Domain;
using Libs.Core.Lifecycle;
using Libs.Core.Primitives;

namespace Features.Score.App
{
    public class MarshalLinesClearedEvents : IInitializable, IDisposable
    {
        private readonly ILinesClearedHandler _linesClearedHandler;
        private readonly IGameplayEventsDispatcher _gameEventsDispatcher;

        public MarshalLinesClearedEvents(ILinesClearedHandler linesClearedHandler, IGameplayEventsDispatcher gameEventsDispatcher)
        {
            _linesClearedHandler = linesClearedHandler;
            _gameEventsDispatcher = gameEventsDispatcher;
        }

        public void Initialize() => 
            _gameEventsDispatcher.OnRowsCleared += HandleRowsCleared;

        public void Dispose() => 
            _gameEventsDispatcher.OnRowsCleared -= HandleRowsCleared;

        private void HandleRowsCleared(UpToFourBytes obj) => 
            _linesClearedHandler.HandleLinesCleared(obj.Count);
    }
}