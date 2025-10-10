using System;
using Features.Gameplay.Domain;
using Libs.Core;

namespace Features.Gameplay.App
{
    public class BoardPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly IGameplayEvents _events;
        private readonly IBoardStateProvider _boardStateProvider;
        private readonly IGameplayBoardDisplay _boardDisplay;
        private bool _isDirty;

        public BoardPresenter(IGameplayEvents events, IBoardStateProvider boardStateProvider, IGameplayBoardDisplay boardDisplay)
        {
            _events = events;
            _boardStateProvider = boardStateProvider;
            _boardDisplay = boardDisplay;
        }

        public void Initialize()
        {
            _boardDisplay.Initialize(_boardStateProvider.BoardState);
            _events.OnBoardStateChanged += MarkForRepaint;
        }

        public void Dispose() => 
            _events.OnBoardStateChanged -= MarkForRepaint;

        public void Tick(float deltaTime)
        {
            if(_isDirty)
                _boardDisplay.SetState(_boardStateProvider.BoardState);
        }

        private void MarkForRepaint() => 
            _isDirty = true;
    }
}