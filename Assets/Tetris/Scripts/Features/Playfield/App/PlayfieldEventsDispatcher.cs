using System;
using Libs.Core.Lifecycle;
using Libs.Core.Primitives;

namespace Features.Playfield.App
{
    public class PlayfieldEventsDispatcher : IPlayfieldEventsDispatcher, IInitializable, IDisposable
    {
        private readonly Domain.IPlayfieldEventsDispatcher _domainEvents;
        public event Action<UpToFourBytes> OnRowsCleared;
        public event Action OnBoardStateChanged;
        public event Action OnGameOver;

        public PlayfieldEventsDispatcher(Domain.IPlayfieldEventsDispatcher domainEvents) => 
            _domainEvents = domainEvents;

        public void Initialize()
        {
            _domainEvents.OnRowsCleared += HandleRowsCleared;
            _domainEvents.OnBoardStateChanged += HandleBoardStateChanged;
            _domainEvents.OnGameOver += HandleGameOver;
        }

        public void Dispose()
        {
            _domainEvents.OnRowsCleared -= HandleRowsCleared;
            _domainEvents.OnBoardStateChanged -= HandleBoardStateChanged;
            _domainEvents.OnGameOver -= HandleGameOver;
        }

        private void HandleRowsCleared(UpToFourBytes obj) => 
            OnRowsCleared?.Invoke(obj);

        private void HandleBoardStateChanged() => 
            OnBoardStateChanged?.Invoke();

        private void HandleGameOver() => 
            OnGameOver?.Invoke();
    }
}