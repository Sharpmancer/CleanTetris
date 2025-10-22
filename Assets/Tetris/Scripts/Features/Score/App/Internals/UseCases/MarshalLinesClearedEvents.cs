using System;
using Features.Playfield.App;
using Features.Score.Domain.Api;
using Libs.Core.Lifecycle;

namespace Features.Score.App.Internals
{
    internal class MarshalLinesClearedEvents : IInitializable, IDisposable
    {
        private readonly ILinesClearedHandler _linesClearedHandler;
        private readonly IPlayfieldEventsDispatcher _gameEventsDispatcher;

        internal MarshalLinesClearedEvents(ILinesClearedHandler linesClearedHandler, IPlayfieldEventsDispatcher gameEventsDispatcher)
        {
            _linesClearedHandler = linesClearedHandler;
            _gameEventsDispatcher = gameEventsDispatcher;
        }

        public void Initialize() => 
            _gameEventsDispatcher.OnRowsCleared += HandleRowsCleared;

        public void Dispose() => 
            _gameEventsDispatcher.OnRowsCleared -= HandleRowsCleared;

        private void HandleRowsCleared(int obj) => 
            _linesClearedHandler.HandleLinesCleared(obj);
    }
}