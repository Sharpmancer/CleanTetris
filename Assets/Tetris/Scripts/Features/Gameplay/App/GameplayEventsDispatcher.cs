using System;
using Features.Gameplay.Domain;
using Libs.Core.Lifecycle;
using Libs.Core.Primitives;

namespace Features.Gameplay.App
{
    public class GameplayEventsDispatcher : IGameplayEventsDispatcher, IInitializable, IDisposable
    {
        private readonly IGameplayEvents _domainEvents;
        public event Action<UpToFourBytes> OnRowsCleared;
        public event Action OnBoardStateChanged;
        public event Action OnGameOver;

        public GameplayEventsDispatcher(IGameplayEvents domainEvents) => 
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