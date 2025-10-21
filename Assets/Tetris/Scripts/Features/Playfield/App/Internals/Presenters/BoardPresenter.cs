using System;
using Features.Playfield.Domain.Api;
using Libs.Core.Lifecycle;

namespace Features.Playfield.App
{
    internal class BoardPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly Domain.Api.IPlayfieldEventsDispatcher _events;
        private readonly IPlayfieldStateProvider _playfieldStateProvider;
        private readonly IPlayfieldDisplay _boardDisplay;
        private bool _isDirty;

        internal BoardPresenter(Domain.Api.IPlayfieldEventsDispatcher events, IPlayfieldStateProvider playfieldStateProvider, IPlayfieldDisplay boardDisplay)
        {
            _events = events;
            _playfieldStateProvider = playfieldStateProvider;
            _boardDisplay = boardDisplay;
        }

        public void Initialize()
        {
            _boardDisplay.Initialize(_playfieldStateProvider.BoardState);
            _events.OnBoardStateChanged += MarkForRepaint;
            // eliminates race condition with gameplay initialization
            MarkForRepaint();
        }

        public void Dispose() => 
            _events.OnBoardStateChanged -= MarkForRepaint;

        public void Tick(float deltaTime)
        {
            if (!_isDirty)
                return;
            
            _boardDisplay.SetState(_playfieldStateProvider.BoardState);
            _isDirty = false;
        }

        private void MarkForRepaint() => 
            _isDirty = true;
    }
}