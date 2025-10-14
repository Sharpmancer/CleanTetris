using System;
using Features.Gameplay.App;
using Features.Score.App;
using Libs.Core;
using Libs.Persistence;

namespace Features.Persistence.App
{
    public class SaveOnGameStateChangedUseCase : IInitializable, IDisposable
    {
        private readonly IGameplayEventsDispatcher _gameEvents;
        private readonly ISnapshotable<GameplaySnapshot> _gameplaySnapshot;
        private readonly ISnapshotable<ScoreSnapshot> _scoreSnapshot;
        private readonly ISaver _saver;

        public SaveOnGameStateChangedUseCase(
            IGameplayEventsDispatcher gameEvents, 
            ISaver saver, 
            ISnapshotable<GameplaySnapshot> gameplaySnapshot,
            ISnapshotable<ScoreSnapshot> scoreSnapshot)
        {
            _gameEvents = gameEvents;
            _saver = saver;
            _gameplaySnapshot = gameplaySnapshot;
            _scoreSnapshot = scoreSnapshot;
        }

        public void Initialize() => 
            _gameEvents.OnBoardStateChanged += SaveGameState;

        public void Dispose() => 
            _gameEvents.OnBoardStateChanged -= SaveGameState;

        private void SaveGameState() => 
            _saver.Save(PersistenceConstants.SAVE_KEY, new SaveDataAssembly(_gameplaySnapshot.GetSnapshot(), _scoreSnapshot.GetSnapshot()));
    }
}