using System;
using Features.Gameplay.Domain;
using Libs.Core;

namespace Features.Gameplay.App
{
    public class GameplayEventsDispatcher : IGameplayEventsDispatcher, IInitializable, IDisposable
    {
        private readonly IGameplayEvents _domainEvents;
        public event Action<UpToFourBytes> OnRowsCleared;

        public GameplayEventsDispatcher(IGameplayEvents domainEvents) => 
            _domainEvents = domainEvents;

        public void Initialize() =>
            _domainEvents.OnRowsCleared += HandleRowsCleared;
        
        public void Dispose() => 
            _domainEvents.OnRowsCleared -= HandleRowsCleared;

        private void HandleRowsCleared(UpToFourBytes obj) => 
            OnRowsCleared?.Invoke(obj);
    }
}