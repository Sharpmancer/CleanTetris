using System;
using Features.Gameplay.Domain;
using Libs.Core;

namespace Features.Gameplay.App
{
    public class GameplayEventsDispatcher : IGameplayEventsDispatcher, IInitializable, IDisposable
    {
        private readonly IGameplayEvents _domainEvents;
        public event Action<UpToFourBytes> OnRowsCleared;
        public event Action OnBoardStateChanged;

        public GameplayEventsDispatcher(IGameplayEvents domainEvents) => 
            _domainEvents = domainEvents;

        public void Initialize()
        {
            _domainEvents.OnRowsCleared += HandleRowsCleared;
            _domainEvents.OnBoardStateChanged += HandleBoardStateChanged;
        }

        public void Dispose()
        {
            _domainEvents.OnRowsCleared -= HandleRowsCleared;
            _domainEvents.OnBoardStateChanged -= HandleBoardStateChanged;
        }

        private void HandleRowsCleared(UpToFourBytes obj) => 
            OnRowsCleared?.Invoke(obj);

        private void HandleBoardStateChanged() => 
            OnBoardStateChanged?.Invoke();
    }
}