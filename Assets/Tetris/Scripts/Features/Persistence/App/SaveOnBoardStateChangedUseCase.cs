using System;
using Features.Gameplay.App;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class SaveOnGameStateChangedUseCase : IInitializable, IDisposable
    {
        private const string SESSION_STATE_KEY = "session_state";
        private readonly IGameplayEventsDispatcher _gameEvents;
        private readonly ISnapshotable<GameplaySnapshot> _gameplaySnapshot;
        private readonly ISaver _saver;

        public SaveOnGameStateChangedUseCase(IGameplayEventsDispatcher gameEvents, ISaver saver, ISnapshotable<GameplaySnapshot> gameplaySnapshot)
        {
            _gameEvents = gameEvents;
            _saver = saver;
            _gameplaySnapshot = gameplaySnapshot;
        }

        public void Initialize() => 
            _gameEvents.OnBoardStateChanged += SaveGameState;

        public void Dispose() => 
            _gameEvents.OnBoardStateChanged -= SaveGameState;

        private void SaveGameState()
        {
            
            _saver.Save(SESSION_STATE_KEY, _gameplaySnapshot.GetSnapshot());
        }
    }
}